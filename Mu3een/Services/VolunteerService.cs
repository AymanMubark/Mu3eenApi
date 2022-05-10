using Mu3een.Data;
using Mu3een.Entities;
using Mu3een.Models;
using Microsoft.EntityFrameworkCore;
using Mu3een.Authorization;
using Mu3een.Helpers;

namespace Mu3een.Services
{
    public interface IVolunteerService
    {
        public Task<string> Login(string phone);

        public Task<VerifyOTPResponseModel> VerifyOTP(string phone, string otp);
        public Task<VolunteerModel> GetVolunteerById(Guid id);
        public Task<VolunteerModel> Register(Guid id, VolunteerRegisterRequestModel  model,string baseUrl);
        public Task<IEnumerable<RewardModel>> GetRewardsById(Guid id);
        public Task<IEnumerable<SocialEventVolunteerModel>> GetSocialEventsById(Guid id);
        public Task<Volunteer> GetById(Guid id);
        public Task<Volunteer?> GetByPhone(string phone);
    }

    public class VolunteerService : IVolunteerService
    {
        public readonly Mu3eenContext _db;
        public readonly IJwtUtils _iJwtUtils;
        public VolunteerService(Mu3eenContext db, IJwtUtils jwtUtils)
        {
            _db = db;
            _iJwtUtils = jwtUtils;
        }

        public async Task<Volunteer> GetById(Guid id)
        {
            Volunteer? volunteer = await _db.Volunteers.FindAsync(id);
            if (volunteer == null) throw new KeyNotFoundException("Volunteer not found");
            return volunteer;
        }
        public async Task<Volunteer?> GetByPhone(string phone)
        {
            return await _db.Volunteers.SingleOrDefaultAsync(x => x.Phone == phone);
        }

        public async Task<VolunteerModel> GetVolunteerById(Guid id)
        {
            return new VolunteerModel(await GetById(id));
        }

        public async Task<string> Login(string phone)
        {
            Volunteer? volunteer = await GetByPhone(phone);
            Random random = new();
            var otp = random.Next(1000, 9999).ToString();
            if (volunteer == null)
            {
                volunteer = new Volunteer() { Phone = phone, OTP = otp, Role = Role.Volunteer };
                await _db.AddAsync(volunteer);
            }
            else
            {
                volunteer.OTP = otp;
                _db.Update(volunteer);
            }
            await _db.SaveChangesAsync();
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
                throw new AppException("OTP not valid");
            }
            return new VerifyOTPResponseModel()
            {
                Token = _iJwtUtils.GenerateJwtToken(volunteer),
                User = new VolunteerModel(volunteer),
                Role = "Volunteer",
            };
        }

        public async Task<IEnumerable<RewardModel>> GetRewardsById(Guid id)
        {
            return await _db.VolunteerRewards.Include(x=>x.Reward).Where(x => x.VolunteerId == id).Select(x => new RewardModel(x.Reward)).ToListAsync();
        }

        public async Task<IEnumerable<SocialEventVolunteerModel>> GetSocialEventsById(Guid id)
        {
            return await _db.SocialEventVolunteers.Include(x=>x.SocialEvent).Include(x=>x.SocialEvent!.SocialEventType).Where(x => x.VolunteerId == id).Select(x => new SocialEventVolunteerModel(x)).ToListAsync();
        }

   
        public async Task<VolunteerModel> Register(Guid id, VolunteerRegisterRequestModel model,string baseUrl)
        {
            var volunteer =  await GetById(id);
            volunteer.Name = model.Name;    
            volunteer.Age = model.Age; 
            volunteer.Gender = model.Gender;
            if (model.Image != null){
                var image = baseUrl + "/" + (await _filesHelper.UploadFile(model.Image));
                volunteer.ImageUrl = image;
            }
            _db.Update(volunteer);
            await _db.SaveChangesAsync();
            return new VolunteerModel(volunteer);
        }
    }
}
