using Microsoft.EntityFrameworkCore;
using Mu3een.Data;
using Mu3een.Entities;
using Mu3een.IServices;

namespace Mu3een.Services
{
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
            return await _db.SocialEventTypes.AsNoTracking().ToListAsync();
        }

        public async Task<SocialEventType> GetById(Guid id)
        {
            SocialEventType? SocialEventType = await _db.SocialEventTypes.FindAsync(id);
            if (SocialEventType == null) throw new KeyNotFoundException("Social Event Type not found");
            return SocialEventType;
        }

        public async Task Delete(Guid id)
        {
            SocialEventType? socialEventType = await _db.SocialEventTypes.FindAsync(id);
            if (socialEventType == null) throw new KeyNotFoundException("Social Event Type not found");
            _db.Remove(socialEventType);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Guid id, SocialEventType model)
        {
            SocialEventType? socialEventType = await _db.SocialEventTypes.FindAsync(id);
            if (socialEventType == null) throw new KeyNotFoundException("Social Event Type not found");
            socialEventType.Name = model.Name;
            socialEventType.NameAr = model.NameAr;
            _db.Update(socialEventType);
            _db.SaveChanges();
        }
    }
}
