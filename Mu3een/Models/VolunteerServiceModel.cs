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
            SocialEvent =new SocialEventModel(model.SocialEvent!);
            Completed = model.Completed;
            CreatedAt = model.CreatedAt;
        }
        public SocialEventModel? SocialEvent { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool Completed { get; set; }
    }
}
