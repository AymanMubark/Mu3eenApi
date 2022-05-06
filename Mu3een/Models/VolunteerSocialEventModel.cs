using Mu3een.Entities;

namespace Mu3een.Models
{
    public class VolunteerSocialEventModel
    {
        public VolunteerSocialEventModel()
        {

        }  
        public VolunteerSocialEventModel(VolunteerSocialEvent model)
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
