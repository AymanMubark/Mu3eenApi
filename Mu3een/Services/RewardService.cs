using Microsoft.EntityFrameworkCore;
using Mu3een.Data;
using Mu3een.Entities;
using Mu3een.Helpers;
using Mu3een.Models;

namespace Mu3een.Services
{
    public interface IRewardService
    {
        public Task<Reward> GetById(Guid id);
        public Task<RewardModel> GetRewardById(Guid id);
        public Task<IEnumerable<RewardModel>> GetAll();
        public Task Add(RewardAddRequestModel model, string baseUrl);
        public Task Delete(Guid id);
        public Task Redeem(Guid id, Guid volunteerId);
    }
    public class RewardService : IRewardService
    {
        private readonly Mu3eenContext _db;
        public readonly FilesHelper _filesHelper;


        public RewardService(Mu3eenContext db, FilesHelper filesHelper)
        {
            _db = db;
            _filesHelper = filesHelper;
        }

        public async Task Add(RewardAddRequestModel model, string baseUrl)
        {
            string? image = null;
            if (model.Image != null)
                image = baseUrl + "/" + (await _filesHelper.UploadFile(model.Image));

            var Reward = await _db.Rewards.AddAsync(new Reward
            {
                InstitutionId = model.InstitutionId,
                Name = model.Name,
                Content = model.Content,
                Numbers = model.Numbers,
                Points = model.Points,
                ImageUrl = image,
                ExpiryDate = model.ExpiryDate,
            });
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<RewardModel>> GetAll()
        {
            return await _db.Rewards.Where(x => x.Status).Select(x => new RewardModel(x)).ToListAsync();
        }

        public async Task<RewardModel> GetRewardById(Guid id)
        {
            return new RewardModel(await GetById(id));
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
                    throw new KeyNotFoundException("reward exp");
                }
                Volunteer? volunteer = await _db.Volunteers.FindAsync(volunteerId);
                if (volunteer != null)
                {
                    if (volunteer.Points < reward.Points)
                    {
                        throw new AppException("points les than institution points");
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
                throw new AppException("reward alerady redeem check rewards list!");
            }
        }

    }
}
