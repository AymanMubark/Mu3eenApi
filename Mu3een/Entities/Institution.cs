namespace Mu3een.Entities
{
    public class Institution : User
    {
        public Institution()
        {
            Role = Role.Institution;
        }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public virtual ICollection<SocialService>? SocialServices { get; set; }
      
    }
}
