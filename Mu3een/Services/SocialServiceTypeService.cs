using Microsoft.EntityFrameworkCore;
using Mu3een.Data;
using Mu3een.Entities;

namespace Mu3een.Services
{
    public interface ISocialServiceTypeService
    {
        public Task<IEnumerable<SocialServiceType>> GetAll();
        public Task<SocialServiceType> GetById(Guid id);
        public Task Add(string Name);
        public Task Update(Guid id,string Name);
        public Task Delete(Guid id);
    }
    public class SocialServiceTypeService : ISocialServiceTypeService
    {
        private readonly Mu3eenContext _db;

        public SocialServiceTypeService(Mu3eenContext db)
        {
            _db = db;
        }

        public async Task Add(string Name)
        {
           var SocialServiceType = await _db.SocialServiceTypes.AddAsync(new SocialServiceType { Name = Name });
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<SocialServiceType>> GetAll()
        {
            return await _db.SocialServiceTypes.ToListAsync();
        }

        public async Task<SocialServiceType> GetById(Guid id)
        {
            SocialServiceType? SocialServiceType = await _db.SocialServiceTypes.FindAsync(id);
            if (SocialServiceType == null) throw new KeyNotFoundException("SocialServiceType not found");
            return SocialServiceType;
        }

        public async Task Delete(Guid id)
        {
            _db.Remove(await GetById(id));
            await _db.SaveChangesAsync();
        }

        public async Task Update(Guid id, string Name)
        {
            SocialServiceType? SocialServiceType = await GetById(id);
            SocialServiceType.Name = Name;
            _db.Update(SocialServiceType);
            _db.SaveChanges();
        }
    }
}
