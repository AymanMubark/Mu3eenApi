using System.ComponentModel.DataAnnotations.Schema;

namespace Mu3een.Entities
{
    public class Reward : Base
    {
        public string? Name { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageId { get; set; }
        public int Points { get; set; } = 0;
        public DateTime? ExpiryDate { get; set; }
        public int? Numbers { get; set; }
        [Column("InstitutionId")]
        public Guid? InstitutionId { get; set; }
        public Institution? Institution { get; set; }

    }
}
