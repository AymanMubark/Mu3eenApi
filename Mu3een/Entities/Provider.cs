namespace Mu3een.Entities
{
    public class Provider : User
    {
        public Provider()
        {
            Role = Role.Provider;
        }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public virtual ICollection<SocialService>? SocialServices { get; set; }
      
    }
}
