namespace Mu3een.Entities
{
    public class Admin : User
    {
        public Admin()
        {
            Role = Role.Admin;
        }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
