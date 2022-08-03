using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Mu3een.Data;
using Mu3een.Entities;
using Mu3een.Errors;
using Mu3een.IServices;
using Mu3een.Models;

namespace Mu3een.Services
{
    public class RewardService : IRewardService
    {
        private readonly Mu3eenContext _db;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;


        public RewardService(Mu3eenContext db, IMapper mapper, IPhotoService photoService)
        {
            _db = db;
            _mapper = mapper;
            _photoService = photoService;
        }

        public async Task Add(RewardAddRequestModel model)
        {

            Reward reward = _mapper.Map<Reward>(model);

            if (model.Image != null)
            {
                var result = await _photoService.AddPhotoAsync(model.Image);
                if (result.Error != null) throw new Exception(result.Error.Message);
                reward.ImageUrl = result.Url.AbsoluteUri;
                reward.ImageId = result.PublicId;
            }
            await _db.Rewards.AddAsync(reward);
            await _db.SaveChangesAsync();
        }

        public async Task<PagedList<RewardModel>> GetAll(RewardSearchModel model)
        {
            var query = _db.Rewards.Where(x => x.Status && x.Name!.ToLower().Contains(model.Key ?? "".ToLower())).AsQueryable();
            
            return await PagedList<RewardModel>.CreateAsync(query
                .ProjectTo<RewardModel>(_mapper.ConfigurationProvider)
                .AsNoTracking(), model.PageNumber, model.PageSize);

        }

        public async Task<int> GetCount()
        {
            return await _db.Rewards.Where(x => x.Status).CountAsync();
        }

        public async Task<RewardModel> GetRewardById(Guid id)
        {
            return _mapper.Map<RewardModel>(await GetById(id));
        }

        public async Task<Reward> GetById(Guid id)
        {
            Reward? Reward = await _db.Rewards.FindAsync(id);
            if (Reward == null) throw new KeyNotFoundException("Reward not found");
            return Reward;
        }

        public async Task Delete(Guid id)
        {
            var reward = await GetById(id);
            reward.Status = false;
            _db.Rewards.Update(reward);
            await _db.SaveChangesAsync();
        }

        public async Task Redeem(Guid id, Guid volunteerId)
        {
            var volunteerReward = await _db.VolunteerRewards.SingleOrDefaultAsync(x => x.VolunteerId == volunteerId && x.RewardId == id);

            if (volunteerReward == null)
            {
                var reward = await _db.Rewards.FindAsync(id);
                if (reward == null)
                {
                    throw new KeyNotFoundException("reward not found");
                }
                Volunteer? volunteer = await _db.Volunteers.FindAsync(volunteerId);
                if (volunteer != null)
                {
                    if (volunteer.Points < reward.Points)
                    {
                        throw new AppException("You don’t have enough points.");
                    }

                    volunteerReward = new VolunteerReward()
                    {
                        VolunteerId = volunteerId,
                        RewardId = id,
                    };

                    await _db.VolunteerRewards.AddAsync(volunteerReward);


                    volunteer.Points -= reward!.Points!;
                    _db.Volunteers.Update(volunteer);

                    await _db.SaveChangesAsync();
                }
            }
            else
            {
                throw new AppException("reward already redeemed check your reward list!");
            }
        }

    }
}
