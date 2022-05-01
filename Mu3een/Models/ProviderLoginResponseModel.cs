namespace Mu3een.Models
{
    public class ProviderLoginResponseModel
    {
        public ProviderModel? User { get; set; }
        public string? Token { get; set; }
        public string? Role { get; set; }
    }
}
