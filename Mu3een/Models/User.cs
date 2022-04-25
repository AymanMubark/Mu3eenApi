using System.ComponentModel.DataAnnotations;

namespace Mu3een.Models
{
    public class User : Base
    {
        [StringLength(50)]
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
    }
}
