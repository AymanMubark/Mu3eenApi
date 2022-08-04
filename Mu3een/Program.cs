using Microsoft.OpenApi.Models;
using Mu3een.Data;
using Mu3een.Extensions;
using Mu3een.Middleware;
using Mu3een.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// configure DI for application services
builder.Services.AddApplicationService(builder.Configuration);
// security
builder.Services.IdentityService(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

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
