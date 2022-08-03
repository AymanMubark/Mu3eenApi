using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Mu3een.Data;
using Mu3een.Entities;
using Mu3een.IServices;
using Mu3een.Models;
using System.Data;

namespace Mu3een.Services
{
    public class SocialEventService : ISocialEventService
    {
        public readonly IPhotoService _photoService;
        private readonly Mu3eenContext _db;
        public readonly IMapper _mapper;

        public SocialEventService(Mu3eenContext db, IPhotoService photoService, IMapper mapper)
        {
            _db = db;
            _photoService = photoService;
            _mapper = mapper;
        }

        public async Task<int> GetCount()
        {
            return await _db.SocialEvents.Where(x => x.Status).CountAsync();
        }

        public async Task Add(SocialEventAddRequestModel model)
        {
            var socialEvent = new SocialEvent
            {
                Name = model.Name,
                Description = model.Description,
                SocialEventTypeId = model.SocialEventTypeId,
                ExpiryDate = model.ExpiryDate,
                VolunteerRequried = model.VolunteerRequried,
                Points = model.Points,
                Address = model.Address,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                InstitutionId = model.InstitutionId,
            };

            if (model.Image != null)
            {
                var result = await _photoService.AddPhotoAsync(model.Image);
                if (result.Error != null) throw new Exception(result.Error.Message);
                socialEvent.ImageUrl = result.Url.AbsoluteUri;
                socialEvent.ImageId = result.PublicId;
            }

            await _db.SocialEvents.AddAsync(socialEvent);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            SocialEvent socialEvent = await GetById(id);
            socialEvent.Status = false;
            _db.Update(socialEvent);
            await _db.SaveChangesAsync();
        }

        public async Task<PagedList<SocialEventModel>> GetAll(SocialEventSearchModel model)
        {
            var query = _db.SocialEvents
                .Include(x => x.SocialEventType)
                .Include(x => x.Institution)
                .Where(x =>
                (x.Name!.ToLower().Contains(model.Key ?? "".ToLower()) ||
                x.Description!.ToLower().Contains(model.Key ?? "".ToLower()) ||
                x.Institution!.UserName!.ToLower().Contains(model.Key ?? "".ToLower())) &&
                x.Address!.StartsWith(model.Address ?? "") &&
                (model.SocialEventTypeid == null || x.SocialEventTypeId == model.SocialEventTypeid) &&
                x.Status)
               .OrderByDescending(x => x.CreatedAt)
               .AsQueryable();

            return await PagedList<SocialEventModel>.CreateAsync(query
                .ProjectTo<SocialEventModel>(_mapper.ConfigurationProvider)
                .AsNoTracking(), model.PageNumber, model.PageSize);

        }

        public async Task<SocialEventModel> GetSocialEventById(Guid id)
        {
            return _mapper.Map<SocialEventModel>(await GetById(id));
        }
        public async Task<SocialEvent> GetById(Guid id)
        {
            SocialEvent? socialEvent = await _db.SocialEvents.FindAsync(id);
            if (socialEvent == null) throw new KeyNotFoundException("Social Service not found");
            return socialEvent;
        }

        public async Task<IEnumerable<SocialEventVolunteerModel>> GetEventVolunteers(Guid id)
        {
            return await _db.SocialEventVolunteers.Include(x => x.Volunteer).Where(x => x.SocialEventId == id)
                .ProjectTo<SocialEventVolunteerModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
        public async Task ApplyTo(Guid id, Guid volunteerId)
        {
            SocialEventVolunteer? socialEventVolunteer = await _db.SocialEventVolunteers.SingleOrDefaultAsync(x => x.VolunteerId == volunteerId && x.SocialEventId == id);
            if (socialEventVolunteer == null)
            {
                int count = await _db.SocialEventVolunteers.Where(x => x.SocialEventId == id && x.VolunteerStatus == VolunteerSocialEventStatus.Accept).CountAsync();
                if ((count + 1) < (await GetById(id)).VolunteerRequried!)
                {
                    await _db.SocialEventVolunteers.AddAsync(new SocialEventVolunteer()
                    {
                        VolunteerId = volunteerId,
                        SocialEventId = id,
                    });
                    await _db.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Volunteers number is full !");
                }
            }
            else
            {
                throw new Exception("has already been requested");
            }
        }

        public async Task SetCompleted(Guid id, Guid volunteerId)
        {
            Entities.SocialEventVolunteer? volunteerSocialEvent = await _db.SocialEventVolunteers.Include(x => x.SocialEvent).SingleOrDefaultAsync(x => x.SocialEventId == id && x.VolunteerId == volunteerId);
            if (volunteerSocialEvent != null)
            {
                if (volunteerSocialEvent.SocialEvent != null)
                {
                    Volunteer? volunteer = await _db.Volunteers.FindAsync(volunteerId);
                    if (volunteer != null)
                    {
                        volunteer.Points += volunteerSocialEvent.SocialEvent.Points;
                        _db.Volunteers.Update(volunteer);

                        volunteerSocialEvent.VolunteerStatus = VolunteerSocialEventStatus.Complete;
                        _db.SocialEventVolunteers.Update(volunteerSocialEvent);

                        await _db.SaveChangesAsync();
                    }
                }

            }
        }

        public async Task SetAccept(Guid id, Guid volunteerId)
        {
            SocialEventVolunteer? volunteerSocialEvent = await _db.SocialEventVolunteers.SingleOrDefaultAsync(x => x.SocialEventId == id && x.VolunteerId == volunteerId);
            if (volunteerSocialEvent != null)
            {
                int count = await _db.SocialEventVolunteers.Where(x => x.SocialEventId == id && x.VolunteerStatus == VolunteerSocialEventStatus.Accept).CountAsync();
                if ((count + 1) < (await GetById(id)).VolunteerRequried!)
                {
                    volunteerSocialEvent.VolunteerStatus = VolunteerSocialEventStatus.Accept;
                    _db.SocialEventVolunteers.Update(volunteerSocialEvent);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Volunteers number is full !");
                }

            }
        }


    }
}
