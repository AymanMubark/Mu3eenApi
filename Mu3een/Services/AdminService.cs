﻿using Mu3een.IServices;
using Microsoft.EntityFrameworkCore;
using Mu3een.Data;
using Mu3een.Entities;
using Mu3een.Interfaces;
using Mu3een.Models;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Mu3een.Errors;

namespace Mu3een.Services
{
    public class AdminService : IAdminService
    {
        public readonly Mu3eenContext _db;
        private readonly IConfiguration _configuration;
        public readonly ITokenService _tokenService;
        private readonly IPhotoService _photoService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AdminService(Mu3eenContext db, IPhotoService photoService, ITokenService tokenService, UserManager<AppUser> userManager, IMapper mapper, IConfiguration configuration)
        {
            _db = db;
            _photoService = photoService;
            _tokenService = tokenService;
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<AdminModel> Add(AdminRequestModel model)
        {

            Admin admin = _mapper.Map<Admin>(model);


            if (model.Image != null)
            {
                var result = await _photoService.AddPhotoAsync(model.Image);
                if (result.Error != null) throw new Exception(result.Error.Message);
                admin.ImageUrl = result.Url.AbsoluteUri;
                admin.ImageId = result.PublicId;
            }

            await _userManager.CreateAsync(admin, model.Password);

            await _userManager.AddToRoleAsync(admin, "Admin");

            return _mapper.Map<AdminModel>(admin);
        }
        public async Task<AdminModel> GetById(Guid id)
        {
            Admin? admin = await _db.Admins.FindAsync(id);
            if (admin == null) throw new KeyNotFoundException("admin not found");
            return _mapper.Map<AdminModel>(admin);
        }

        public async Task<AdminModel> Update(Guid id, AdminUpdateRequestModel model)
        {
            Admin? admin = await _db.Admins.FindAsync(id);
            if (admin == null) throw new KeyNotFoundException("Admin not found");
            admin.Name = model.Name;
            admin.Email = model.Email;
            if (model.Image != null)
            {
                var result = await _photoService.AddPhotoAsync(model.Image);
                if (result.Error != null) throw new Exception(result.Error.Message);
                admin.ImageUrl = result.Url.AbsoluteUri;
                admin.ImageId = result.PublicId;
            }
            _db.Update(admin);
            await _db.SaveChangesAsync();
            return _mapper.Map<AdminModel>(admin);
        }

        public async Task<AdminLoginResponseModel> Login(AdminLoginRequestModel model)
        {

            Admin? admin = await _db.Admins.AsNoTracking().SingleOrDefaultAsync(x => x.UserName == model.Username);

            if (admin == null)
            {
                throw new KeyNotFoundException("admin invalid");
            }

            if (!await _userManager.CheckPasswordAsync(admin, model.Password))
            {
                throw new AppException("login invalid");
            }

            return new AdminLoginResponseModel()
            {
                Token = await _tokenService.CreateToken(admin),
                User = _mapper.Map<AdminModel>(admin)
            };
        }

        public async Task<PagedList<AdminModel>> GetAll(AdminSearchModel model)
        {
            var query = _db.Admins.Where(x => x.Name!.ToLower().Contains(model.Key ?? "".ToLower())
            || x.UserName!.ToLower().Contains(model.Key ?? "".ToLower())).AsQueryable();

            return await PagedList<AdminModel>.CreateAsync(query
             .ProjectTo<AdminModel>(_mapper.ConfigurationProvider)
             .AsNoTracking(), model.PageNumber, model.PageSize);
        }

        public async Task<AdminCountsReportModel> GetAdminCountsReport()
        {
            var VolunteersCount = await _db.Volunteers.Where(x => x.Status && x.Name != null).AsNoTracking().CountAsync();
            var InstitutionsCount = await _db.Institutions.Where(x => x.Status).AsNoTracking().CountAsync();
            var RewardsCount = await _db.Rewards.Where(x => x.Status).AsNoTracking().CountAsync();
            return new AdminCountsReportModel()
            {
                Volunteers = VolunteersCount,
                Institutions = InstitutionsCount,
                Rewards = RewardsCount,
            };
        }

        public async Task<SocailEventsReport> GetSocailEventsReport()
        {
            List<SocailEventTypeCount> socailEventTypeCount = await _db.SocialEvents.Include(x => x.SocialEventType)
                .Where(x => x.Status)
                .GroupBy(x => x.SocialEventTypeId).AsNoTracking()
                .Select(x => new SocailEventTypeCount
            {
                Count = x.Count(),
                Name = x.First().SocialEventType!.Name,
            }).ToListAsync();

            return new SocailEventsReport()
            {
                Total = socailEventTypeCount.Select(x => x.Count).Sum(),
                TypesCount = socailEventTypeCount.ToList(),
            };
        }
    }
}