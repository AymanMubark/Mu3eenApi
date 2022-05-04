namespace Mu3een.Models
{
    public class InstitutionRegisterModel
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public IFormFile? Image { get; set; }
    }
}
