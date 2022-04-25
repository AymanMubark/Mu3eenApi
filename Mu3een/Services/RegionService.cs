using Microsoft.EntityFrameworkCore;
using Mu3een.Data;
using Mu3een.Entities;

namespace Mu3een.Services
{
    public interface IRegionService
    {
        public Task<IEnumerable<Region>> GetAll();
        public Task<Region> GetById(Guid id);
        public Task Add(string Name);
        public Task Update(Guid id,string Name);
        public Task Delete(Guid id);
    }
    public class RegionService : IRegionService
    {
        private readonly Mu3eenContext _db;

        public RegionService(Mu3eenContext db)
        {
            _db = db;
        }

        public async Task Add(string Name)
        {
           var region = await _db.Regions.AddAsync(new Region { Name = Name });
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Region>> GetAll()
        {
            return await _db.Regions.ToListAsync();
        }

        public async Task<Region> GetById(Guid id)
        {
            Region? region = await _db.Regions.FindAsync(id);
            if (region == null) throw new KeyNotFoundException("Region not found");
            return region;
        }

        public async Task Delete(Guid id)
        {
            _db.Remove(await GetById(id));
            await _db.SaveChangesAsync();
        }

        public async Task Update(Guid id, string Name)
        {
            Region? region = await GetById(id);
            region.Name = Name;
            _db.Update(region);
            _db.SaveChanges();
        }
    }
}
