using EgeBilgiTaskCase.Application.Common.DTOs.RickAndMorty;
using EgeBilgiTaskCase.Infrastructure;
using EgeBilgiTaskCase.Infrastructure.Services;
using HospitalManagement.API.Extensions.StartupExtensions;
using HospitalManagement.Application;
using HospitalManagement.Persistence;
using System.Text.Json.Serialization;
using System.Text.Json;
using EgeBilgiTaskCase.Application.Abstractions.Services.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPersistenceServices();
builder.Services.HttpLoggingOptionsStartupExtension();
builder.Services.JsonOptionsStartupExtension();
builder.Services.AddEndpointsApiExplorer();
builder.Services.SwaggerOptionsExtension();
builder.Services.JwtOptionsStartupExtension(builder.Configuration);
builder.Services.AddScoped<IRickAndMortyApiService, RickAndMortyApiService>();


var app = builder.Build();

app.UseCors(x => x
          .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/egeBilgiTaskCase_V1/swagger.json", "EgeBilgiTaskCase V1");
        c.RoutePrefix = string.Empty;
    });
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
