
using Mu3een.Entities;

namespace Mu3een.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
