using System.ComponentModel.DataAnnotations;

namespace Mu3een.Entities
{
    public enum Gender
    {
        Male,Female
    }
    public class Volunteer : User
    {
        public Volunteer()
        {
            Role = Role.Volunteer;
        }
        public string? Phone { get; set; }
        [MaxLength(4)]
        public string? OTP { get; set; }
        public Gender? Gender { get; set; }
        public int? Age { get; set; }
        public int Points { get; set; } = 0;
        public virtual ICollection<VolunteerSocialEvent>? Services { get; set; }
        public virtual ICollection<VolunteerReward>? Rewards { get; set; }
    }
}
