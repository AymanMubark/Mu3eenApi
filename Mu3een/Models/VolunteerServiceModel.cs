using Mu3een.Entities;

namespace Mu3een.Models
{
    public class VolunteerServiceModel
    {
        public VolunteerServiceModel()
        {

        }  
        public VolunteerServiceModel(VolunteerSocialEvent model)
        {
            Id = model.Id;
            Volunteer =new VolunteerModel(model.Volunteer!);
            Status = model.Status;
        }
        public Guid? Id { get; set; }
        public VolunteerModel? Volunteer { get; set; }
        public VolunteerSocialEventStatus? Status { get; set; }
    }
}
