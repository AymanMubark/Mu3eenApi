using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Mu3een.Data;
using Mu3een.Extensions;
using Mu3een.Middleware;
using Mu3een.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// configure DI for application services
builder.Services.AddApplicationService(builder.Configuration);
builder.Services.IdentityService(builder.Configuration);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
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

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();


app.UseAuthentication();

app.UseAuthorization();

// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// global error handler

app.MapControllers();

await app.initalApp();

app.Run();
