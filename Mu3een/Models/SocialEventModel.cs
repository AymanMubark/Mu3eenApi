using Mu3een.Entities;

namespace Mu3een.Models
{
    public class SocialEventModel
    {
        public SocialEventModel()
        {

        }
        public SocialEventModel(SocialEvent model)
        {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
            InstitutionId = model.InstitutionId;
            SocialEventType = new SocialEventTypeModel(model.SocialEventType!);
            //Region = new RegionModel(model.Region!);
            VolunteerRequried = model.VolunteerRequried;
            Points = model.Points;
            ExpiryDate = model.ExpiryDate;
            Address = model.Address;
            Latitude = model.Latitude;
            Longitude = model.Longitude;
            CreatedAt = model.CreatedAt;
        }
        public Guid? Id { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public Guid? InstitutionId { get; set; }
        public SocialEventTypeModel? SocialEventType { get; set; }
        public RegionModel? Region { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int? VolunteerRequried { get; set; }
        public int? Points { get; set; }
        public string? Address { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

    }
}
