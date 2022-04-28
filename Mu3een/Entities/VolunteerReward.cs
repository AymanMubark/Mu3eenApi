using System.ComponentModel.DataAnnotations.Schema;

namespace Mu3een.Entities
{
    public class VolunteerReward : Base
    {
        [Column("VolunteerId")]
        public Guid? VolunteerId { get; set; }
        public Volunteer? Volunteer { get; set; }
        [Column("RewardId")]
        public Guid? RewardId { get; set; }
        public Reward? Reward { get; set; }
        public bool? Turned { get; set; } = false;
    }
}
