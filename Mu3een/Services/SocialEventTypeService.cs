using Microsoft.EntityFrameworkCore;
using Mu3een.Data;
using Mu3een.Entities;

namespace Mu3een.Services
{
    public interface ISocialEventTypeService
    {
        public Task<IEnumerable<SocialEventType>> GetAll();
        public Task<SocialEventType> GetById(Guid id);
        public Task Add(SocialEventType model);
        public Task Update(Guid id, SocialEventType model);
        public Task Delete(Guid id);
    }
    public class SocialEventTypeService : ISocialEventTypeService
    {
        private readonly Mu3eenContext _db;

        public SocialEventTypeService(Mu3eenContext db)
        {
            _db = db;
        }

        public async Task Add(SocialEventType model)
        {
            var SocialEventType = await _db.SocialEventTypes.AddAsync(new SocialEventType { Name = model.Name, NameAr = model.NameAr, });
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<SocialEventType>> GetAll()
        {
            return await _db.SocialEventTypes.ToListAsync();
        }

        public async Task<SocialEventType> GetById(Guid id)
        {
            SocialEventType? SocialEventType = await _db.SocialEventTypes.FindAsync(id);
            if (SocialEventType == null) throw new KeyNotFoundException("Social Event Type not found");
            return SocialEventType;
        }

        public async Task Delete(Guid id)
        {
            _db.Remove(await GetById(id));
            await _db.SaveChangesAsync();
        }

        public async Task Update(Guid id, SocialEventType model)
        {
            SocialEventType? SocialEventType = await GetById(id);
            SocialEventType.Name = model.Name;
            SocialEventType.NameAr = model.NameAr;
            _db.Update(SocialEventType);
            _db.SaveChanges();
        }
    }
}
