using Mu3een.Data;
using Mu3een.Entities;
using Mu3een.Models;
using Microsoft.EntityFrameworkCore;
using Mu3een.Authorization;
using Mu3een.Helpers;

namespace Mu3een.Services
{
    public interface IProviderService
    {
        public Task<ProviderLoginResponseModel> Login(string email, string password);
        public Task<ProviderLoginResponseModel> Register(ProviderRegisterModel model, string baseUrl);
        public Task<IEnumerable<RewardModel>> GetRewardsById(Guid id);
        public Task<IEnumerable<SocialServiceModel>> GetSocialServicesById(Guid id);
        public Task<ProviderModel> GetProviderById(Guid id);
        public Task<Provider> GetById(Guid id);
    }
    public class ProviderService : IProviderService
    {
        public readonly Mu3eenContext _db;
        public readonly IJwtUtils _iJwtUtils;
        public readonly FilesHelper _filesHelper;
        public ProviderService(Mu3eenContext db, IJwtUtils jwtUtils, FilesHelper filesHelper)
        {
            _db = db;
            _iJwtUtils = jwtUtils;
            _filesHelper = filesHelper;
        }

        public async Task<Provider> GetById(Guid id)
        {
            Provider? provider = await _db.Providers.FindAsync(id);
            if (provider == null) throw new KeyNotFoundException("provider not found");
            return provider;
        }

        public async Task<ProviderModel> GetProviderById(Guid id)
        {
            return new ProviderModel(await GetById(id));
        }

        public async Task<ProviderLoginResponseModel> Login(string email, string password)
        {
            Provider? provider = await _db.Providers.SingleOrDefaultAsync(x => x.Email == email && x.Password == password);

            if (provider == null)
            {
                throw new AppException("login invalid");
            }
            return new ProviderLoginResponseModel()
            {
                Token = _iJwtUtils.GenerateJwtToken(provider),
                User = new ProviderModel(provider),
                Role = "Provider",
            };
        }

        public async Task<ProviderLoginResponseModel> Register(ProviderRegisterModel model, string baseUrl)
        {
            Provider? provider = await _db.Providers.SingleOrDefaultAsync(x => x.Email == model.Email);
            if (provider != null)
            {
                throw new AppException("email already exists");
            }
            string? image = null;
            if (model.Image != null)
                image = baseUrl + "/" + (await _filesHelper.UploadFile(model.Image));
            provider = new Provider()
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                ImageUrl = image,
                Role = Role.Provider,
                Phone =model.Phone,
            };
            await _db.Providers.AddAsync(provider);
            await _db.SaveChangesAsync();
            return await Login(model.Email!, model.Password!);
        }

        public async Task<IEnumerable<RewardModel>> GetRewardsById(Guid id)
        {
            return await _db.Rewards.Where(x => x.ProviderId == id).Select(x => new RewardModel(x)).ToListAsync();
        }
        public async Task<IEnumerable<SocialServiceModel>> GetSocialServicesById(Guid id)
        {
            return await _db.SocialServices.Where(x => x.ProviderId == id).Select(x => new SocialServiceModel(x)).ToListAsync();
        }
    }
}