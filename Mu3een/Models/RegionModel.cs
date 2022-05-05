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
            NameAr = model.NameAr;
        }
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? NameAr { get; set; }
    }
}
