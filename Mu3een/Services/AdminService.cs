using Microsoft.EntityFrameworkCore;
using Mu3een.Authorization;
using Mu3een.Data;
using Mu3een.Entities;
using Mu3een.Helpers;
using Mu3een.Models;

namespace Mu3een.Services
{
    public interface IAdminService
    {
        public Task<AdminLoginResponseModel> Login(AdminLoginRequestModel model);
        public Task<AdminModel> Add(Admin model);
    }
    public class AdminService : IAdminService
    {
        public readonly Mu3eenContext _db;
        public readonly IJwtUtils _iJwtUtils;

        public AdminService(Mu3eenContext db, IJwtUtils jwtUtils)
        {
            _db = db;
            _iJwtUtils = jwtUtils;
        }

        public async Task<AdminModel> Add(Admin model)
        {
            await _db.AddAsync(model);
            await _db.SaveChangesAsync();
            return new AdminModel(model);
        }

        public async Task<AdminLoginResponseModel> Login(AdminLoginRequestModel model)
        {
            Admin? admin = await _db.Admins.SingleOrDefaultAsync(x => x.UserName == model.Username && x.Password == model.Password);

            if (admin == null)
            {
                throw new AppException("login invalid");
            }
            return new AdminLoginResponseModel()
            {
                Token = _iJwtUtils.GenerateJwtToken(admin),
                User = new AdminModel(admin),
                Role = nameof(admin.Role),
            };
        }
    }
}