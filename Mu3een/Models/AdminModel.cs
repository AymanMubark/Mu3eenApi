using Mu3een.Entities;

namespace Mu3een.Models
{
    public class AdminModel
    {
        public AdminModel()
        {

        }
        public AdminModel(Admin model)
        {
            Id = model.Id;
            Name = model.Name;
            UserName = model.UserName;
            Email = model.Email;
            ImageUrl = model.ImageUrl;
            CreatedAt = model.CreatedAt;
        }
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
