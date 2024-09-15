using EgeBilgiTaskCase.Application.Abstractions.Services.Common;
using EgeBilgiTaskCase.Infrastructure;
using EgeBilgiTaskCase.Infrastructure.Services;
using EgeBilgiTaskCase.Infrastructure.Services.Storage.Local;
using EgeBilgiTaskCase.Persistence.Context;
using HospitalManagement.API.Extensions.StartupExtensions;
using HospitalManagement.Application;
using HospitalManagement.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddStorage<LocalStorage>();
builder.Services.AddPersistenceServices();
builder.Services.HttpLoggingOptionsStartupExtension();
builder.Services.JsonOptionsStartupExtension();
builder.Services.AddEndpointsApiExplorer();
builder.Services.SwaggerOptionsExtension();
builder.Services.JwtOptionsStartupExtension(builder.Configuration);
builder.Services.AddScoped<IRickAndMortyApiService, RickAndMortyApiService>();


var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/egeBilgiTaskCase_V1/swagger.json", "EgeBilgiTaskCase V1");
    c.RoutePrefix = string.Empty;
});

app.UseCors(x => x
          .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using (var scoped = app.Services.CreateScope())
{
    var context = scoped.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}

app.Run();
