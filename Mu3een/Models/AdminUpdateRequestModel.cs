namespace Mu3een.Models
{
    public class AdminUpdateRequestModel
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public IFormFile? Image { get; set; }
    }
}
