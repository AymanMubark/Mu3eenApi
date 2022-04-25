using System.ComponentModel.DataAnnotations.Schema;

namespace Mu3een.Entities
{
    public class SocialService : Base
    {
        public string? Name { get; set; }
        [Column("ProviderId")]
        public Guid? ProviderId { get; set; }
        public Provider? Provider { get; set; }    

        [Column("SocialServiceTypeId")]
        public Guid? SocialServiceTypeId { get; set; }
        public SocialServiceType? SocialServiceType { get; set; }   
        [Column("RegionId")]
        public Guid? RegionId { get; set; }
        public Region? Region { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int? VolunteerRequried { get; set; }
        public int? Points { get; set; }
        public string? Address { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
