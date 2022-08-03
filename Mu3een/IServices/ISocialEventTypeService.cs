using Mu3een.Entities;

namespace Mu3een.IServices
{
    public interface ISocialEventTypeService
    {
        public Task<IEnumerable<SocialEventType>> GetAll();
        public Task<SocialEventType> GetById(Guid id);
        public Task Add(SocialEventType model);
        public Task Update(Guid id, SocialEventType model);
        public Task Delete(Guid id);
    }
}
