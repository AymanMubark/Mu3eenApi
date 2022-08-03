using Mu3een.Entities;
using Mu3een.Models;

namespace Mu3een.IServices
{
    public interface ISocialEventService
    {
        public Task ApplyTo(Guid id, Guid volunteerId);
        public Task SetCompleted(Guid id, Guid socialEventId);
        public Task<int> GetCount();
        public Task SetAccept(Guid id, Guid socialEventId);
        public Task<PagedList<SocialEventModel>> GetAll(SocialEventSearchModel model);
        public Task<IEnumerable<SocialEventVolunteerModel>> GetEventVolunteers(Guid id);
        public Task<SocialEventModel> GetSocialEventById(Guid id);
        public Task<SocialEvent> GetById(Guid id);
        public Task Add(SocialEventAddRequestModel model);
        public Task Delete(Guid id);
    }
}
