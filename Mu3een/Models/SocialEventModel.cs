using Mu3een.Entities;

namespace Mu3een.Models
{
    public class SocialEventModel
    {
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
        public string? imageUrl { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

    }
}
