using Mu3een.Entities;

namespace Mu3een.Models
{
    public class InstitutionModel
    {
        public InstitutionModel()
        {

        }
        public InstitutionModel(Institution institution)
        {
            Id = institution.Id;
            Name = institution.Name;
            Phone = institution.Phone;
            Email = institution.Email;
            ImageUrl = institution.ImageUrl;
            CreatedAt = institution.CreatedAt;
        }
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
