using Mu3een.Entities;

namespace Mu3een.Models
{
    public class RewardModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public int? Points { get; set; }
        public int? Numbers { get; set; }
        public Guid? InstitutionId { get; set; }
        public DateTime? CreatedAt { get; set; }

    }
}
