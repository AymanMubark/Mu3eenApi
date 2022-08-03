using Mu3een.Entities;
using Mu3een.Models;

namespace Mu3een.IServices
{
    public interface IInstitutionService
    {
        public Task<InstitutionModel> Update(Guid Id, InstitutionRegisterModel model);
        public Task<InstitutionLoginResponseModel> Register(InstitutionRegisterModel model);
        public Task<InstitutionLoginResponseModel> Login(string email, string password);
        public Task<PagedList<InstitutionModel>> GetAll(InstitutionSearchModel model);
        public Task<PagedList<SocialEventModel>> GetSocialEventsById(Guid id, PaginationParams model);
        public Task<PagedList<RewardModel>> GetRewardsById(Guid id, PaginationParams model);
        public Task<InstitutionModel> GetInstitutionById(Guid id);
        public Task<Institution> GetById(Guid id);
        public Task<int> GetCount();
    }
}