using System.ComponentModel.DataAnnotations;

namespace Mu3een.Models
{
    public enum Gender
    {
        Male,Female
    }
    public class Volunteer : User
    {
        public string? Phone { get; set; }
        [MaxLength(4)]
        public string? OTP { get; set; }
        public Gender? Gender { get; set; }
        public int? Age { get; set; }
    }
}
