using Mu3een.Data;
using API.Helpers;
using Mu3een.Mapper;
using Mu3een.Services;
using Mu3een.IServices;
using Mu3een.Interfaces;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

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


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mu3een App", Version = "v1" });
                c.AddSecurityDefinition("Bearer",
                 new OpenApiSecurityScheme
                 {
                     In = ParameterLocation.Header,
                     Description = "Bearer Token need to be Inserted..",
                     Name = "Authorization",
                     Type = SecuritySchemeType.ApiKey,
                     Scheme = "Bearer"
                 });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                    {
                        new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }});
            });


            return services;
        }
    }
}
