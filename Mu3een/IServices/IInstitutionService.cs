using Mu3een.Entities;
using Mu3een.Models;

namespace Mu3een.IServices
{
    public interface IInstitutionService
    {
        public Task<InstitutionModel> Update(Guid Id, InstitutionRegisterModel model);
        public Task<InstitutionLoginResponseModel> Register(InstitutionRegisterModel model);
        public Task<InstitutionLoginResponseModel> Login(string email, string password);
        public Task<List<InstitutionModel>> GetAll(InstitutionSearchModel model);
        public Task<IEnumerable<SocialEventModel>> GetSocialEventsById(Guid id);
        public Task<IEnumerable<RewardModel>> GetRewardsById(Guid id);
        public Task<InstitutionModel> GetInstitutionById(Guid id);
        public Task<Institution> GetById(Guid id);
        public Task<int> GetCount();
    }
}