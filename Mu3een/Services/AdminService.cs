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
        public Task<IEnumerable<AdminModel>> GetAll(AdminSearchModel model);
        public Task<AdminModel> Add(Admin model);
        public Task<AdminModel> Update(Guid id,AdminUpdateRequestModel model,string baseUrl);
        public Task<Admin> GetById(Guid id);
    }
    public class AdminService : IAdminService
    {
        public readonly Mu3eenContext _db;
        public readonly IJwtUtils _iJwtUtils;
        public readonly FilesHelper _filesHelper;

        public AdminService(Mu3eenContext db, IJwtUtils jwtUtils, FilesHelper filesHelper)
        {
            _db = db;
            _iJwtUtils = jwtUtils;
            _filesHelper = filesHelper;
        }

        public async Task<AdminModel> Add(Admin model)
        {
            await _db.AddAsync(model);
            await _db.SaveChangesAsync();
            return new AdminModel(model);
        }
        public async Task<Admin> GetById(Guid id)
        {
            Admin? admin = await _db.Admins.FindAsync(id);
            if (admin == null) throw new KeyNotFoundException("admin not found");
            return admin;
        }

        public async Task<AdminModel> Update(Guid id, AdminUpdateRequestModel model, string baseUrl)
        {
            Admin admin = await GetById(id);
            admin.Name = model.Name;
            admin.UserName = model.UserName;
            admin.Email = model.Email;
            admin.Password = model.Password;
            if (model.Image != null)
            {
                var image = baseUrl + "/" + (await _filesHelper.UploadFile(model.Image));
                admin.ImageUrl = image;
            }
            _db.Update(admin);
            await _db.SaveChangesAsync();
            return new AdminModel(admin);
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
                Role = nameof(Role.Admin),
            };
        }

        public async Task<IEnumerable<AdminModel>> GetAll(AdminSearchModel model)
        {
            return await _db.Admins.Where(x => x.Name!.ToLower().Contains(model.Key ?? "".ToLower()) || x.UserName!.ToLower().Contains(model.Key ?? "".ToLower())).Select(x => new AdminModel(x)).ToListAsync();
        }
    }
}