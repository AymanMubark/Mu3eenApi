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
        public Task<VolunteerModel> Register(Guid id, VolunteerRegisterRequestModel  model);
        public Task<IEnumerable<VolunteerRewardModel>> GetRewardsById(Guid id);
        public Task<IEnumerable<SocialEventVolunteerModel>> GetSocialEventsById(Guid id);
        public Task<Volunteer> GetById(Guid id);
        public Task<Volunteer?> GetByPhone(string phone);
        public Task ExChangePoints(Guid id, Guid rewardId);
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

        public async Task<IEnumerable<VolunteerRewardModel>> GetRewardsById(Guid id)
        {
            return await _db.VolunteerRewards.Where(x => x.VolunteerId == id).Select(x => new VolunteerRewardModel(x)).ToListAsync();
        }

        public async Task<IEnumerable<Models.SocialEventVolunteerModel>> GetSocialEventsById(Guid id)
        {
            return await _db.SocialEventVolunteers.Where(x => x.VolunteerId == id).Select(x => new SocialEventVolunteerModel(x)).ToListAsync();
        }

        public async Task ExChangePoints(Guid id, Guid rewardId)
        {
            var reward = await _db.Rewards.FindAsync(rewardId);
            if (reward == null)
            {
                throw new KeyNotFoundException("reward exp");
            }
            Volunteer? volunteer = await GetById(id);
            if(volunteer.Points < reward.Points)
            {
                throw new AppException("points les than institution points");
            }
            var volunteerReward = await _db.VolunteerRewards.SingleOrDefaultAsync(x => x.VolunteerId == id && x.RewardId == rewardId);
            if (volunteerReward == null)
            {
                volunteerReward = new VolunteerReward()
                {
                    VolunteerId = id,
                    RewardId = rewardId,
                };

                await _db.VolunteerRewards.AddAsync(volunteerReward);


                volunteer.Points -= reward!.Points!;
                _db.Volunteers.Update(volunteer);

                await _db.SaveChangesAsync();
            }
        }

        public async Task<VolunteerModel> Register(Guid id, VolunteerRegisterRequestModel model)
        {
            var volunteer =  await GetById(id);
            volunteer.Name = model.Name;    
            _db.Update(volunteer);
            await _db.SaveChangesAsync();
            return new VolunteerModel(volunteer);
        }
    }
}