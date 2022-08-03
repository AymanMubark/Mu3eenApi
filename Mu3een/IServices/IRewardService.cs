using Mu3een.Entities;
using Mu3een.Models;

namespace Mu3een.IServices
{
    public interface IRewardService
    {
        public Task<Reward> GetById(Guid id);
        public Task<RewardModel> GetRewardById(Guid id);
        public Task<int> GetCount();
        public Task<PagedList<RewardModel>> GetAll(RewardSearchModel model);
        public Task Add(RewardAddRequestModel model);
        public Task Delete(Guid id);
        public Task Redeem(Guid id, Guid volunteerId);
    }
}
