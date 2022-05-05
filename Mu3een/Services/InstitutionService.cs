﻿using Mu3een.Data;
using Mu3een.Entities;
using Mu3een.Models;
using Microsoft.EntityFrameworkCore;
using Mu3een.Authorization;
using Mu3een.Helpers;

namespace Mu3een.Services
{
    public interface IInstitutionService
    {
        public Task<InstitutionLoginResponseModel> Login(string email, string password);
        public Task<InstitutionLoginResponseModel> Register(InstitutionRegisterModel model, string baseUrl);
        public Task<IEnumerable<RewardModel>> GetRewardsById(Guid id);
        public Task<IEnumerable<SocialEventModel>> GetSocialEventsById(Guid id);
        public Task<InstitutionModel> GetInstitutionById(Guid id);
        public Task<Institution> GetById(Guid id);
    }
    public class InstitutionService : IInstitutionService
    {
        public readonly Mu3eenContext _db;
        public readonly IJwtUtils _iJwtUtils;
        public readonly FilesHelper _filesHelper;
        public InstitutionService(Mu3eenContext db, IJwtUtils jwtUtils, FilesHelper filesHelper)
        {
            _db = db;
            _iJwtUtils = jwtUtils;
            _filesHelper = filesHelper;
        }

        public async Task<Institution> GetById(Guid id)
        {
            Institution? institution = await _db.Institutions.FindAsync(id);
            if (institution == null) throw new KeyNotFoundException("institution not found");
            return institution;
        }

        public async Task<InstitutionModel> GetInstitutionById(Guid id)
        {
            return new InstitutionModel(await GetById(id));
        }

        public async Task<InstitutionLoginResponseModel> Login(string email, string password)
        {
            Institution? institution = await _db.Institutions.SingleOrDefaultAsync(x => x.Email == email && x.Password == password);

            if (institution == null)
            {
                throw new AppException("login invalid");
            }
            return new InstitutionLoginResponseModel()
            {
                Token = _iJwtUtils.GenerateJwtToken(institution),
                User = new InstitutionModel(institution),
                Role = "Institution",
            };
        }

        public async Task<InstitutionLoginResponseModel> Register(InstitutionRegisterModel model, string baseUrl)
        {
            Institution? institution = await _db.Institutions.SingleOrDefaultAsync(x => x.Email == model.Email);
            if (institution != null)
            {
                throw new AppException("email already exists");
            }
            string? image = null;
            if (model.Image != null)
                image = baseUrl + "/" + (await _filesHelper.UploadFile(model.Image));
            institution = new Institution()
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                Role = Role.Institution,
                Phone =model.Phone,
                ImageUrl = image,
            };
            await _db.Institutions.AddAsync(institution);
            await _db.SaveChangesAsync();
            return await Login(model.Email!, model.Password!);
        }

        public async Task<IEnumerable<RewardModel>> GetRewardsById(Guid id)
        {
            return await _db.Rewards.Where(x => x.InstitutionId == id).Select(x => new RewardModel(x)).ToListAsync();
        }
        public async Task<IEnumerable<SocialEventModel>> GetSocialEventsById(Guid id)
        {
            return await _db.SocialEvents.Where(x => x.InstitutionId == id).Select(x => new SocialEventModel(x)).ToListAsync();
        }
    }
}