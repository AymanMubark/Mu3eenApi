using Mu3een.Entities;

namespace Mu3een.Models
{
    public class SocialEventVolunteerModel
    {
        public Guid? Id { get; set; }
        public VolunteerModel? Volunteer { get; set; }
        public SocialEventModel? SocialEvent { get; set; }
        public VolunteerSocialEventStatus? VolunteerStatus { get; set; }
    }
}
