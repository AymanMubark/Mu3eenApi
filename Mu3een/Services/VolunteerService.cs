using Mu3een.Data;
using Mu3een.Entities;
using Mu3een.Models;
using Microsoft.EntityFrameworkCore;
using Mu3een.IServices;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Mu3een.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Mu3een.Services
{

    public class VolunteerService : IVolunteerService
    {
        public readonly Mu3eenContext _db;
        public readonly IMapper _mapper;
        public readonly ITokenService _tokenService;
        public readonly IPhotoService _photoService;
        private readonly UserManager<AppUser> _userManager;

        public VolunteerService(Mu3eenContext db, ITokenService tokenService, IPhotoService photoService, IMapper mapper, UserManager<AppUser> userManager)
        {
            _db = db;
            _tokenService = tokenService;
            _photoService = photoService;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<Volunteer> GetById(Guid id)
        {
            Volunteer? volunteer = await _db.Volunteers.FindAsync(id);
            if (volunteer == null) throw new KeyNotFoundException("Volunteer not found");
            return volunteer;
        }
        public async Task<Volunteer?> GetByPhone(string phone)
        {
            return await _db.Volunteers.SingleOrDefaultAsync(x => x.PhoneNumber == phone);
        }

        public async Task<VolunteerModel> GetVolunteerById(Guid id)
        {
            Volunteer? volunteer = await _db.Volunteers.FindAsync(id);
            return _mapper.Map<VolunteerModel>(volunteer);
        }

        public async Task<string> VerifyPhone(string phone)
        {
            Volunteer? volunteer = await GetByPhone(phone);
            Random random = new();
            var otp = random.Next(1000, 9999).ToString();
            if (volunteer == null)
            {
                volunteer = new Volunteer() { OTP = otp, UserName = phone };

                var result = await _userManager.CreateAsync(volunteer);
                if (result.Succeeded)
                {
                    await _userManager.SetPhoneNumberAsync(volunteer, phone);

                    await _userManager.AddToRoleAsync(volunteer, "Volunteer");
                }
                else
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
            else
            {
                volunteer.OTP = otp;
                _db.Update(volunteer);
                await _db.SaveChangesAsync();
            }
            //Send OTP IN SMS 
            return otp;
        }

        public async Task<VerifyOTPResponseModel> VerifyOTP(string phone, string otp)
        {
            Volunteer? volunteer = await GetByPhone(phone);
            if (volunteer == null)
            {
                throw new KeyNotFoundException("Volunteer not found");
            }
            if (volunteer.OTP != otp)
            {
                throw new Exception("OTP isn't valid");
            }

            var token = await _tokenService.CreateToken(volunteer);

            volunteer.PhoneNumberConfirmed = true;

            await _userManager.UpdateAsync(volunteer);

            return new VerifyOTPResponseModel()
            {
                User = _mapper.Map<VolunteerModel>(volunteer),
                Token = token,
            };
        }
        public async Task<int> GetCount()
        {
            return await _db.Volunteers.Where(x => x.Status && x.UserName != null).CountAsync();
        }

        public async Task<IEnumerable<RewardModel>> GetRewardsById(Guid id)
        {
            return await _db.VolunteerRewards.Include(x => x.Reward)
                .Where(x => x.VolunteerId == id)
                .AsNoTracking()
                .ProjectTo<RewardModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<IEnumerable<SocialEventVolunteerModel>> GetSocialEventsById(Guid id)
        {
            return await _db.SocialEventVolunteers.Include(x => x.SocialEvent)
                .ThenInclude(x => x!.SocialEventType)
                .Where(x => x.VolunteerId == id)
                .ProjectTo<SocialEventVolunteerModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }


        public async Task<VolunteerModel> Register(Guid id, VolunteerRegisterRequestModel model)
        {

            Volunteer? volunteer = await _db.Volunteers.FindAsync(id);
            if(volunteer == null) throw new Exception();
            volunteer.Name = model.Name;
            volunteer.Age = model.Age;
            volunteer.Gender = model.Gender;

            if (model.Image != null)
            {
                if (model.Image != null)
                {
                    var result = await _photoService.AddPhotoAsync(model.Image);
                    if (result.Error != null) throw new Exception(result.Error.Message);
                    volunteer.ImageUrl = result.Url.AbsoluteUri;
                    volunteer.ImageId = result.PublicId;
                }
            }

            _db.Volunteers.Update(volunteer);
            await _db.SaveChangesAsync();

            return _mapper.Map<VolunteerModel>(volunteer);

        }

        public Task<List<VolunteerModel>> GetAll(VolunteerSearchModel model)
        {
            return _db.Volunteers.Where(x => x.UserName != null
                   && (x.UserName.ToLower().Contains(model.Key ?? "".ToLower())
                   || x.PhoneNumber!.ToLower().Contains(model.Key ?? "".ToLower())))
                .AsNoTracking()
                .ProjectTo<VolunteerModel>(_mapper.ConfigurationProvider).AsNoTracking().ToListAsync();
        }
    }
}
