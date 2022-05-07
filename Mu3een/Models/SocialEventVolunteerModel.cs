using Mu3een.Entities;

namespace Mu3een.Models
{
    public class SocialEventVolunteerModel
    {
        public SocialEventVolunteerModel()
        {

        }  
        public SocialEventVolunteerModel(SocialEventVolunteer model)
        {
            Id = model.Id;
            Volunteer =new VolunteerModel(model.Volunteer!);
            SocialEvent = new SocialEventModel(model.SocialEvent!);
            VolunteerStatus = model.VolunteerStatus;
        }
        public Guid? Id { get; set; }
        public VolunteerModel? Volunteer { get; set; }
        public SocialEventModel? SocialEvent { get; set; }
        public VolunteerSocialEventStatus? VolunteerStatus { get; set; }
    }
}
