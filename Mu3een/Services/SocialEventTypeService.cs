using Microsoft.EntityFrameworkCore;
using Mu3een.Data;
using Mu3een.Entities;

namespace Mu3een.Services
{
    public interface ISocialEventTypeService
    {
        public Task<IEnumerable<SocialEventType>> GetAll();
        public Task<SocialEventType> GetById(Guid id);
        public Task Add(string Name);
        public Task Update(Guid id,string Name);
        public Task Delete(Guid id);
    }
    public class SocialEventTypeService : ISocialEventTypeService
    {
        private readonly Mu3eenContext _db;

        public SocialEventTypeService(Mu3eenContext db)
        {
            _db = db;
        }

        public async Task Add(string Name)
        {
           var SocialEventType = await _db.SocialEventTypes.AddAsync(new SocialEventType { Name = Name });
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<SocialEventType>> GetAll()
        {
            return await _db.SocialEventTypes.ToListAsync();
        }

        public async Task<SocialEventType> GetById(Guid id)
        {
            SocialEventType? SocialEventType = await _db.SocialEventTypes.FindAsync(id);
            if (SocialEventType == null) throw new KeyNotFoundException("SocialEventType not found");
            return SocialEventType;
        }

        public async Task Delete(Guid id)
        {
            _db.Remove(await GetById(id));
            await _db.SaveChangesAsync();
        }

        public async Task Update(Guid id, string Name)
        {
            SocialEventType? SocialEventType = await GetById(id);
            SocialEventType.Name = Name;
            _db.Update(SocialEventType);
            _db.SaveChanges();
        }
    }
}
