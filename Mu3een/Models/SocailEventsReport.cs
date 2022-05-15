namespace Mu3een.Models
{
    public class SocailEventsReport
    {
        public int? Total { get; set; }
        public List<SocailEventTypeCount>? TypesCount { get; set; }
    }
    public class SocailEventTypeCount
    {
        public string? Name { get; set; }
        public int? Count { get; set; }
    }
}
