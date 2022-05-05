using System.ComponentModel.DataAnnotations.Schema;

namespace Mu3een.Entities
{
    public class SocialEvent : Base
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Column("InstitutionId")]
        public Guid? InstitutionId { get; set; }
        public Institution? Institution { get; set; }    

        [Column("SocialEventTypeId")]
        public Guid? SocialEventTypeId { get; set; }
        public SocialEventType? SocialEventType { get; set; }   
        [Column("RegionId")]
        public Guid? RegionId { get; set; }
        public Region? Region { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int? VolunteerRequried { get; set; }
        public int Points { get; set; } = 0;
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
