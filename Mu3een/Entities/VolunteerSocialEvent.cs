using System.ComponentModel.DataAnnotations.Schema;

namespace Mu3een.Entities
{
    public class VolunteerSocialEvent : Base
    {
        [Column("VolunteerId")]
        public Guid? VolunteerId { get; set; }
        public Volunteer? Volunteer { get; set; }
        [Column("SocialEventId")]
        public Guid? SocialEventId { get; set; }
        public SocialEvent? SocialEvent { get; set; }
        public bool Completed { get; set; } = false;
    }
}
