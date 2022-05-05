using Mu3een.Entities;

namespace Mu3een.Models
{
    public class RegionModel
    {
        public RegionModel() { }
        public RegionModel(Region model)
        {
            Id = model.Id;
            Name = model.Name;
        }
        public Guid? Id { get; set; }
        public string? Name { get; set; }
    }
}
