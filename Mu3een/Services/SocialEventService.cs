using Microsoft.EntityFrameworkCore;
using Mu3een.Data;
using Mu3een.Entities;
using Mu3een.Helpers;
using Mu3een.Models;

namespace Mu3een.Services
{
    public interface ISocialEventService
    {
        public Task<IEnumerable<SocialEventModel>> GetAll();
        public Task<IEnumerable<SocialEventModel>> GetAllByInstitutionId(Guid id);
        public Task<IEnumerable<Models.VolunteerSocialEventModel>> GetEventsVolunteersById(Guid id);
        public Task<SocialEventModel> GetSocialEventById(Guid id);
        public Task<SocialEvent> GetById(Guid id);
        public Task Add(SocialEventAddRequestModel model, string baseUrl);
        public Task Delete(Guid id);
    }
    public class SocialEventService : ISocialEventService
    {
        private readonly Mu3eenContext _db;
        public readonly FilesHelper _filesHelper;
        public readonly IVolunteerService _volunteerService;

        public SocialEventService(Mu3eenContext db, FilesHelper filesHelper, IVolunteerService volunteerService)
        {
            _db = db;
            _filesHelper = filesHelper;
            _volunteerService = volunteerService;
        }

        public async Task Add(SocialEventAddRequestModel model, string baseUrl)
        {
            string? image = null;
            if (model.Image != null)
                image = baseUrl + "/" + (await _filesHelper.UploadFile(model.Image));
            await _db.SocialEvents.AddAsync(new SocialEvent()
            {
                Name = model.Name, 
                Address = model.Address,
                ExpiryDate = model.ExpiryDate,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Description = model.Description,
                InstitutionId = model.InstitutionId,
                RegionId = model.RegionId,
                SocialEventTypeId = model.SocialEventTypeId,
                VolunteerRequried = model.VolunteerRequried,
                Points = model.Points,
                ImageUrl = image
            });
            await _db.SaveChangesAsync();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SocialEventModel>> GetAll()
        {
            return await _db.SocialEvents.Include(x=>x.SocialEventType).Select(x => new SocialEventModel(x)).ToListAsync();
        }

        public async Task<IEnumerable<SocialEventModel>> GetAllByInstitutionId(Guid id)
        {
            return await _db.SocialEvents.Where(x => x.InstitutionId == id).Select(x => new SocialEventModel(x)).ToListAsync();
        }

        public async Task<SocialEventModel> GetSocialEventById(Guid id)
        {
            return new SocialEventModel(await GetById(id));
        }
        public async Task<SocialEvent> GetById(Guid id)
        {
            SocialEvent? socialEvent = await _db.SocialEvents.FindAsync(id);
            if (socialEvent == null) throw new KeyNotFoundException("Social Service not found");
            return socialEvent;
        }

        public async Task<IEnumerable<Models.VolunteerSocialEventModel>> GetEventsVolunteersById(Guid id)
        {
            return await _db.VolunteerSocialEvents.Where(x => x.SocialEventId == id).Select(x => new VolunteerSocialEventModel(x)).ToListAsync();
        }

      
    }
}
