using Mu3een.Entities;

namespace Mu3een.Models
{
    public class SocialEventVolunteerModel
    {
        public SocialEventVolunteerModel()
        {

        }  
        public SocialEventVolunteerModel(VolunteerSocialEvent model)
        {
            Id = model.Id;
            Volunteer =new VolunteerModel(model.Volunteer!);
            VolunteerStatus = model.VolunteerStatus;
        }
        public Guid? Id { get; set; }
        public VolunteerModel? Volunteer { get; set; }
        public VolunteerSocialEventStatus? VolunteerStatus { get; set; }
    }
}
