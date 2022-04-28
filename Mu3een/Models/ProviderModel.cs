using Mu3een.Entities;

namespace Mu3een.Models
{
    public class ProviderModel
    {
        public ProviderModel()
        {

        }
        public ProviderModel(Provider provider)
        {
            Id = provider.Id;
            Name = provider.Name;
            Phone = provider.Phone;
            Email = provider.Email;
            ImageUrl = provider.ImageUrl;
        }
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? ImageUrl { get; set; }
    }
}
