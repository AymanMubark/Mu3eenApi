using Mu3een.Data;
using Mu3een.Mapper;
using Mu3een.Services;
using Mu3een.IServices;
using Mu3een.Interfaces;
using Microsoft.EntityFrameworkCore;
using API.Helpers;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Mu3een.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISocialEventTypeService, SocialEventTypeService>();
            services.AddScoped<ISocialEventService, SocialEventService>();
            services.AddScoped<IVolunteerService, VolunteerService>();
            services.AddScoped<IInstitutionService, InstitutionService>();
            services.AddScoped<IRewardService, RewardService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddAutoMapper(typeof(MapperProfile).Assembly);

            services.AddDbContext<Mu3eenContext>(option => option.
            UseSqlServer(Configuration.GetConnectionString("Mu3eenContext")));

            return services;
        }
    }
}
