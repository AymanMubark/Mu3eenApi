namespace Mu3een.Entities
{
    public class Institution : AppUser
    {
        public virtual ICollection<SocialEvent>? SocialEvents { get; set; }
      
    }
}
