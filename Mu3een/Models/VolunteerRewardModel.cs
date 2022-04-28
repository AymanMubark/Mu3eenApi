using Mu3een.Entities;

namespace Mu3een.Models
{
    public class VolunteerRewardModel
    {
        public VolunteerRewardModel() { }
        public VolunteerRewardModel(VolunteerReward model)
        {
            Reward = new RewardModel(model.Reward!);
            CreatedAt = model.CreatedAt;
        }
        public RewardModel? Reward { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
