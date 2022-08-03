namespace Mu3een.Models
{
    public class SocialEventSearchModel : PaginationParams
    {
        public string? Key { get; set; } = "";
        public string? Address { get; set; } = "";
        public Guid? SocialEventTypeid { get; set; }
    }
}
