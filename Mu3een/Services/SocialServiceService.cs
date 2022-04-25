using Microsoft.EntityFrameworkCore;
using Mu3een.Data;
using Mu3een.Entities;

namespace Mu3een.Services
{
    public interface ISocialServiceService
    {
        public Task<IEnumerable<SocialService>> GetAll();
        public Task<SocialService> GetById(Guid id);
        public Task Add(string Name);
        public Task Update(Guid id,string Name);
        public Task Delete(Guid id);
    }
    public class SocialServiceService : ISocialServiceService
    {
        private readonly Mu3eenContext _db;

        public SocialServiceService(Mu3eenContext db)
        {
            _db = db;
        }

        public async Task Add(string Name)
        {
           var SocialService = await _db.SocialServices.AddAsync(new SocialService { Name = Name });
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<SocialService>> GetAll()
        {
            return await _db.SocialServices.ToListAsync();
        }

        public async Task<SocialService> GetById(Guid id)
        {
            SocialService? SocialService = await _db.SocialServices.FindAsync(id);
            if (SocialService == null) throw new KeyNotFoundException("SocialService not found");
            return SocialService;
        }

        public async Task Delete(Guid id)
        {
            _db.Remove(await GetById(id));
            await _db.SaveChangesAsync();
        }

        public async Task Update(Guid id, string Name)
        {
            SocialService? SocialService = await GetById(id);
            SocialService.Name = Name;
            _db.Update(SocialService);
            _db.SaveChanges();
        }
    }
}
