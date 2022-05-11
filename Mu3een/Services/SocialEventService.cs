﻿using Microsoft.EntityFrameworkCore;
using Mu3een.Data;
using Mu3een.Entities;
using Mu3een.Helpers;
using Mu3een.Models;

namespace Mu3een.Services
{
    public interface ISocialEventService
    {
        public Task ApplyToService(Guid id, Guid volunteerId);
        public Task SetCompleted(Guid id, Guid socialEventId);
        public Task SetAccept(Guid id, Guid socialEventId);
        public Task<IEnumerable<SocialEventModel>> GetAll(SocialEventSearchModel model);
        public Task<IEnumerable<SocialEventVolunteerModel>> GetEventVolunteers(Guid id);
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

        public async Task Delete(Guid id)
        {
            SocialEvent socialEvent =await GetById(id);
            socialEvent.Status = false;
             _db.Update(socialEvent);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<SocialEventModel>> GetAll(SocialEventSearchModel model)
        {
            return await _db.SocialEvents
                .Include(x => x.SocialEventType)
                .Include(x=>x.Institution)
                .Where(x=>
                x.Name!.ToLower().Contains(model.Key??"".ToLower()) ||
                x.Description!.ToLower().Contains(model.Key??"") || 
                x.Institution!.Name!.ToLower().Contains(model.Key??"") &&
                x.Address!.StartsWith(model.Address ?? "") &&
                (model.SocialEventTypeid == null || x.SocialEventTypeId == model.SocialEventTypeid) &&
                x.Status)
               .OrderByDescending(x=>x.CreatedAt)
               .Select(x => new SocialEventModel(x))
               .ToListAsync();
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

        public async Task<IEnumerable<SocialEventVolunteerModel>> GetEventVolunteers(Guid id)
        {
            return await _db.SocialEventVolunteers.Include(x=> x.Volunteer).Where(x => x.SocialEventId == id).Select(x => new SocialEventVolunteerModel(x)).ToListAsync();
        }
        public async Task ApplyToService(Guid id, Guid volunteerId)
        {
            SocialEventVolunteer? socialEventVolunteer =await _db.SocialEventVolunteers.SingleOrDefaultAsync(x => x.VolunteerId == volunteerId && x.SocialEventId == id);
            if (socialEventVolunteer == null)
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
                throw new AppException("Alerdy Requested");
            }
        }

        public async Task SetCompleted(Guid id, Guid volunteerId)
        {
            Entities.SocialEventVolunteer? volunteerSocialEvent = await _db.SocialEventVolunteers.Include(x=>x.SocialEvent).SingleOrDefaultAsync(x => x.SocialEventId == id && x.VolunteerId == volunteerId);
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
                volunteerSocialEvent.VolunteerStatus = VolunteerSocialEventStatus.Accept;
                _db.SocialEventVolunteers.Update(volunteerSocialEvent);
                await _db.SaveChangesAsync();

            }
        }


    }
}
