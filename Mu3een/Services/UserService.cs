using Mu3een.Data;
using Mu3een.Entities;

namespace Mu3een.Services
{
    public interface IUserService
    {
        public User? GetById(Guid id);
    }
    public class UserService : IUserService
    {
        public readonly Mu3eenContext _db;
        public UserService(Mu3eenContext db)
        {
            _db = db;
        }

        public User? GetById(Guid id)
        {
            return _db.Users.Find(id);
        }
    }
}