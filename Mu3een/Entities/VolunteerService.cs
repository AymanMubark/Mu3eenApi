using System.ComponentModel.DataAnnotations.Schema;

namespace Mu3een.Entities
{
    public class VolunteerService : Base
    {
        [Column("VolunteerId")]
        public Guid? VolunteerId { get; set; }
        public Volunteer? Volunteer { get; set; }
        [Column("SocialServiceId")]
        public Guid? SocialServiceId { get; set; }
        public SocialService? SocialService { get; set; }
    }
}
