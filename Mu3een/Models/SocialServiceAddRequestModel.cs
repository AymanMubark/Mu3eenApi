namespace Mu3een.Models
{
    public class SocialServiceAddRequestModel
    {
        public string? Name { get; set; }
        public Guid? InstitutionId { get; set; }
        public Guid? SocialServiceTypeId { get; set; }
        public Guid? RegionId { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int? VolunteerRequried { get; set; }
        public IFormFile? Image { get; set; }
        public int Points { get; set; }
        public string? Address { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
