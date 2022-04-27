using System.ComponentModel.DataAnnotations;

namespace Mu3een.Models
{
    public class VerifyOTPModel
    {
        [Required]
        public string Phone { get; set; }
        [Required]
        public string OTP { get; set; }
    }
}
