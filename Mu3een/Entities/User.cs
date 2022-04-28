using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Mu3een.Entities
{
    public class User : Base
    {
        [StringLength(50)]
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? ImageUrl { get; set; }
        public Role Role { get; set; }
    }
}
