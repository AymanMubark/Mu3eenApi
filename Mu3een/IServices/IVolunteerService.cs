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
        public Task<PagedList<VolunteerModel>> GetAll(VolunteerSearchModel model);
        public Task<VolunteerModel> Register(Guid id, VolunteerRegisterRequestModel model);
        public Task<PagedList<RewardModel>> GetRewardsById(Guid id, PaginationParams model);
        public Task<PagedList<SocialEventVolunteerModel>> GetSocialEventsById(Guid id, PaginationParams model);
        public Task<Volunteer> GetById(Guid id);
        public Task<Volunteer?> GetByPhone(string phone);
    }
}
