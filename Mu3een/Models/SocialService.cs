using System.ComponentModel.DataAnnotations.Schema;

namespace Mu3een.Models
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
        public DateTime? ExpiryDate { get; set; }
        public int? NumberVolunteerRequried { get; set; }
    }
}
