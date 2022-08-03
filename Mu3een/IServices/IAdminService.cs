using Mu3een.Entities;
using Mu3een.Models;

namespace Mu3een.IServices
{
    public interface IAdminService
    {
        public Task<AdminLoginResponseModel> Login(AdminLoginRequestModel model);
        public Task<AdminCountsReportModel> GetAdminCountsReport();
        public Task<SocailEventsReport> GetSocailEventsReport();
        public Task<IEnumerable<AdminModel>> GetAll(AdminSearchModel model);
        public Task<AdminModel> Add(AdminRequestModel model);
        public Task<AdminModel> Update(Guid id, AdminUpdateRequestModel model);
        public Task<Admin> GetById(Guid id);
    }
}