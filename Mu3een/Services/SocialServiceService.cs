using Microsoft.EntityFrameworkCore;
using Mu3een.Data;
using Mu3een.Entities;
using Mu3een.Helpers;
using Mu3een.Models;

namespace Mu3een.Services
{
    public interface ISocialServiceService
    {
        public Task<IEnumerable<SocialServiceModel>> GetAll();
        public Task<IEnumerable<SocialServiceModel>> GetAllByInstitutionId(Guid id);
        public Task<IEnumerable<VolunteerServiceModel>> GetServicesVolunteersById(Guid id);
        public Task<SocialServiceModel> GetSocialServiceById(Guid id);
        public Task<SocialService> GetById(Guid id);
        public Task Add(SocialServiceAddRequestModel model, string baseUrl);
        public Task Delete(Guid id);
    }
    public class SocialServiceService : ISocialServiceService
    {
        private readonly Mu3eenContext _db;
        public readonly FilesHelper _filesHelper;
        public readonly IVolunteerService _volunteerService;

        public SocialServiceService(Mu3eenContext db, FilesHelper filesHelper, IVolunteerService volunteerService)
        {
            _db = db;
            _filesHelper = filesHelper;
            _volunteerService = volunteerService;
        }

        public async Task Add(SocialServiceAddRequestModel model, string baseUrl)
        {
            string? image = null;
            if (model.Image != null)
                image = baseUrl + "/" + (await _filesHelper.UploadFile(model.Image));
            await _db.SocialServices.AddAsync(new SocialService()
            {
                Address = model.Address,
                ExpiryDate = model.ExpiryDate,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                InstitutionId = model.InstitutionId,
                RegionId = model.RegionId,
                SocialServiceTypeId = model.SocialServiceTypeId,
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

        public async Task<IEnumerable<SocialServiceModel>> GetAll()
        {
            return await _db.SocialServices.Select(x => new SocialServiceModel(x)).ToListAsync();
        }

        public async Task<IEnumerable<SocialServiceModel>> GetAllByInstitutionId(Guid id)
        {
            return await _db.SocialServices.Where(x => x.InstitutionId == id).Select(x => new SocialServiceModel(x)).ToListAsync();
        }

        public async Task<SocialServiceModel> GetSocialServiceById(Guid id)
        {
            return new SocialServiceModel(await GetById(id));
        }
        public async Task<SocialService> GetById(Guid id)
        {
            SocialService? socialService = await _db.SocialServices.FindAsync(id);
            if (socialService == null) throw new KeyNotFoundException("Social Service not found");
            return socialService;
        }

        public async Task<IEnumerable<VolunteerServiceModel>> GetServicesVolunteersById(Guid id)
        {
            return await _db.VolunteerSocialServices.Where(x => x.SocialServiceId == id).Select(x => new VolunteerServiceModel(x)).ToListAsync();
        }

      
    }
}
