using Mu3een.Entities;
using Mu3een.Models;

namespace Mu3een.IServices
{
    public interface IVolunteerService
    {
        public Task<string> VerifyPhone(string phone);
        public Task<int> GetCount();
        public Task<VerifyOTPResponseModel> VerifyOTP(string phone, string otp);
        public Task<VolunteerModel> GetVolunteerById(Guid id);
        public Task<List<VolunteerModel>> GetAll(VolunteerSearchModel model);
        public Task<VolunteerModel> Register(Guid id, VolunteerRegisterRequestModel model);
        public Task<IEnumerable<RewardModel>> GetRewardsById(Guid id);
        public Task<IEnumerable<SocialEventVolunteerModel>> GetSocialEventsById(Guid id);
        public Task<Volunteer> GetById(Guid id);
        public Task<Volunteer?> GetByPhone(string phone);
    }
}
