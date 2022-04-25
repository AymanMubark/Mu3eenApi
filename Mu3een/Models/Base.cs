using System.ComponentModel.DataAnnotations;

namespace Mu3een.Models
{
    public class Base
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; } = DateTime.Now;
        public bool Status { get; set; } = true;
    }
}
