using Mu3een.Entities;

namespace Mu3een.Models
{
    public class SocialEventTypeModel
    {
        public SocialEventTypeModel() { }
        public SocialEventTypeModel(SocialEventType model)
        {
            Id = model.Id;
            Name = model.Name;
        }
        public Guid? Id { get; set; }
        public string? Name { get; set; }
    }
}
