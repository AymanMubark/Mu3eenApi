using Mu3een.Data;
using Mu3een.Entities;
using Mu3een.IServices;

namespace Mu3een.Services
{
    public class UserService : IUserService
    {
        public readonly Mu3eenContext _db;
        public UserService(Mu3eenContext db)
        {
            _db = db;
        }

        public AppUser? GetById(Guid id)
        {
            return _db.Users.Find(id);
        }
    }
}