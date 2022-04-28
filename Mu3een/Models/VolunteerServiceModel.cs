using Mu3een.Entities;

namespace Mu3een.Models
{
    public class VolunteerServiceModel
    {
        public VolunteerServiceModel()
        {

        }  
        public VolunteerServiceModel(VolunteerSocialService model)
        {
            SocialService =new SocialServiceModel(model.SocialService!);
            Completed = model.Completed;
            CreatedAt = model.CreatedAt;
        }
        public SocialServiceModel? SocialService { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool Completed { get; set; }
    }
}
