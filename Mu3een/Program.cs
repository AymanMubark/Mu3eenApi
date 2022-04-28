using Microsoft.EntityFrameworkCore;
using Mu3een.Authorization;
using Mu3een.Data;
using Mu3een.Helpers;
using Mu3een.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
    var services = builder.Services;
    services.AddControllers().AddJsonOptions(x =>
    {
    // serialize enums as strings in api responses (e.g. Role)
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

    services.AddDbContext<Mu3eenContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Mu3eenContext")));
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddHttpContextAccessor();

    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    // configure DI for application services
    services.AddScoped<IJwtUtils, JwtUtils>();
    services.AddScoped<FilesHelper>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IRegionService, RegionService>();
    services.AddScoped<ISocialServiceTypeService, SocialServiceTypeService>();
    services.AddScoped<ISocialServiceService, SocialServiceService>();
    services.AddScoped<IVolunteerService, VolunteerService>();
    services.AddScoped<IProviderService, ProviderService>();
    services.AddScoped<IRewardService, RewardService>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
