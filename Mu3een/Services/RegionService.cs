using Microsoft.EntityFrameworkCore;
using Mu3een.Data;
using Mu3een.Entities;

namespace Mu3een.Services
{
    public interface IRegionService
    {
        public Task<IEnumerable<Region>> GetAll();
        public Task<Region> GetById(Guid id);
        public Task Add(Region model);
        public Task Update(Guid id, Region model);
        public Task Delete(Guid id);
    }
    public class RegionService : IRegionService
    {
        private readonly Mu3eenContext _db;

        public RegionService(Mu3eenContext db)
        {
            _db = db;
        }

        public async Task Add(Region model)
        {
           var region = await _db.Regions.AddAsync(new Region { Name = model.Name, NameAr = model.NameAr});
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

        public async Task Update(Guid id, Region model)
        {
            Region? region = await GetById(id);
            region.Name = model.Name;
            region.NameAr = model.NameAr;
            _db.Update(region);
            _db.SaveChanges();
        }
    }
}
