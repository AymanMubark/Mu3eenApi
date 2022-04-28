using Mu3een.Entities;

namespace Mu3een.Models
{
    public class SocialServiceModel
    {
        public SocialServiceModel()
        {

        }
        public SocialServiceModel(SocialService model)
        {
            Name = model.Name;
            Provider = new ProviderModel(model.Provider!);
            SocialServiceType = model.SocialServiceType;
            Region = model.Region;
            VolunteerRequried = model.VolunteerRequried;
            Points = model.Points;
            ExpiryDate = model.ExpiryDate;
            Address = model.Address;
            Latitude = model.Latitude;
            Longitude = model.Longitude;
            CreatedAt = model.CreatedAt;
        }
        public string? Id { get; set; }
        public string? Name { get; set; }
        public ProviderModel? Provider { get; set; }
        public SocialServiceType? SocialServiceType { get; set; }
        public Region? Region { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int? VolunteerRequried { get; set; }
        public int? Points { get; set; }
        public string? Address { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

    }
}
