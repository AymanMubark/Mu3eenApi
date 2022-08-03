using Mu3een.Entities;

namespace Mu3een.IServices
{
    public interface IUserService
    {
        public AppUser? GetById(Guid id);
    }
}