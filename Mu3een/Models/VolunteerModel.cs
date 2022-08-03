using Mu3een.Entities;

namespace Mu3een.Models
{
    public class VolunteerModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? ImageUrl { get; set; }
        public Gender? Gender { get; set; }
        public int? Age { get; set; }
        public int Points { get; set; } = 0;
        public DateTime? CreatedAt { get; set; }
    }
}
