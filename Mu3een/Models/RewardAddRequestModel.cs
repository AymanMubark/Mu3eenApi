namespace Mu3een.Models
{

    public class RewardAddRequestModel
    {
        public Guid? InstitutionId { get; set; }
        public string? Name { get; set; }
        public string? Content { get; set; }
        public IFormFile? Image { get; set; }
        public int Points { get; set; }
        public int? Numbers { get; set; }
    }
}
