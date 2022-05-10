
using Mu3een.Entities;

namespace Mu3een.Models
{
    public class VolunteerRegisterRequestModel
    {
        public string? Name { get; set; }
        public IFormFile? Image { get; set; }
        public Gender? Gender { get; set; }
        public int? Age { get; set; }
    }
}
