
using Mu3een.Entities;

namespace Mu3een.Models
{
    public class AdminRequestModel
    {
        public string? Name { get; set; }
        public IFormFile? Image { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
    }
}
