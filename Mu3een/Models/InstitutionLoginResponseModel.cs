namespace Mu3een.Models
{
    public class InstitutionLoginResponseModel
    {
        public InstitutionModel? User { get; set; }
        public string? Token { get; set; }
        public string? Role { get; set; }
    }
}
