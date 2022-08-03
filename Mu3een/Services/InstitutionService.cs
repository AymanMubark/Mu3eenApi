using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Mu3een.Entities;
using Mu3een.Models;
using Mu3een.Data;
using AutoMapper;
using Mu3een.Interfaces;
using Mu3een.IServices;
using Microsoft.AspNetCore.Identity;

namespace Mu3een.Services
{
    public class InstitutionService : IInstitutionService
    {
        public readonly Mu3eenContext _db;
        public readonly IMapper _mapper;
        public readonly ITokenService _tokenService;
        public readonly IPhotoService _photoService;
        private readonly UserManager<AppUser> _userManager;


        public InstitutionService(Mu3eenContext db, ITokenService tokenService, IPhotoService photoService, IMapper mapper, UserManager<AppUser> userManager)
        {
            _db = db;
            _tokenService = tokenService;
            _photoService = photoService;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<int> GetCount()
        {
            return await _db.Institutions.Where(x => x.Status).CountAsync();
        }
        public async Task<Institution> GetById(Guid id)
        {
            Institution? institution = await _db.Institutions.FindAsync(id);
            if (institution == null) throw new KeyNotFoundException("institution not found");
            return institution;
        }

        public async Task<InstitutionModel> GetInstitutionById(Guid id)
        {
            return _mapper.Map<InstitutionModel>(await GetById(id));
        }

        public async Task<InstitutionLoginResponseModel> Login(string email, string password)
        {
            Institution? institution = await _db.Institutions.SingleOrDefaultAsync(x => x.Email == email);

            if (institution == null)
            {
                throw new KeyNotFoundException("user invalid");
            }

            if (!await _userManager.CheckPasswordAsync(institution, password))
            {
                throw new Exception("login invalid");
            }

            return new InstitutionLoginResponseModel()
            {
                Token = await _tokenService.CreateToken(institution),
                User = _mapper.Map<InstitutionModel>(institution),
            };
        }

        public async Task<InstitutionLoginResponseModel> Register(InstitutionRegisterModel model)
        {
            Institution? institution = await _db.Institutions.SingleOrDefaultAsync(x => x.Email == model.Email);
            if (institution != null)
            {
                throw new Exception("email already registerd");
            }

            institution =new Institution
            {
                UserName = model.Phone,
                Email = model.Email,
                Name = model.Name,
                PhoneNumber = model.Phone,
            };

            if (model.Image != null)
            {
                var result = await _photoService.AddPhotoAsync(model.Image);
                if (result.Error != null) throw new Exception(result.Error.Message);
                institution.ImageUrl = result.Url.AbsoluteUri;
                institution.ImageId = result.PublicId;
            }

            await _userManager.CreateAsync(institution, model.Password);

            await _userManager.AddToRoleAsync(institution, "Institution");

            return new InstitutionLoginResponseModel()
            {
                Token = await _tokenService.CreateToken(institution),
                User = _mapper.Map<InstitutionModel>(institution),
            };
        }

        public async Task<InstitutionModel> Update(Guid Id, InstitutionRegisterModel model)
        {
            Institution? institution = await GetById(Id);


            institution.UserName = model.Name;
            institution.Email = model.Email;

            if (model.Image != null)
            {
                var result = await _photoService.AddPhotoAsync(model.Image);
                if (result.Error != null) throw new Exception(result.Error.Message);
                institution.ImageUrl = result.Url.AbsoluteUri;
                institution.ImageId = result.PublicId;
            }

            _db.Institutions.Update(institution);
            await _db.SaveChangesAsync();
            return _mapper.Map<InstitutionModel>(institution);
        }

        public async Task<IEnumerable<RewardModel>> GetRewardsById(Guid id)
        {
            return await _db.Rewards.Where(x => x.InstitutionId == id)
                 .ProjectTo<RewardModel>(_mapper.ConfigurationProvider)
                 .AsNoTracking()
                 .ToListAsync();
        }
        public async Task<IEnumerable<SocialEventModel>> GetSocialEventsById(Guid id)
        {
            return await _db.SocialEvents
                .Include(x => x.SocialEventType)
                .Where(x => x.InstitutionId == id && x.Status)
                .ProjectTo<SocialEventModel>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<List<InstitutionModel>> GetAll(InstitutionSearchModel model)
        {
            return _db.Institutions.Where(x => x.UserName!.ToLower().Contains(model.Key ?? "".ToLower()) || x.PhoneNumber!.ToLower().Contains(model.Key ?? "".ToLower()))
                .ProjectTo<InstitutionModel>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}