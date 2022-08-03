namespace Mu3een.Models
{
    public class VerifyOTPResponseModel
    {
        public VolunteerModel? User { get; set; }
        public string? Token { get; set; }
    }
}
