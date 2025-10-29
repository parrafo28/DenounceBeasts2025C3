using DenounceBeasts.API.Middleware;
using DenounceBeasts.API.Models;
using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infrasctructure;
using DenounceBeasts.Infrasctructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<SectorRepository>();
//builder.Services.AddTransient<ComplaintTypeRepository>();
builder.Services.AddTransient<GenericRepository<ComplaintType>>();
builder.Services.AddTransient<MunicipalityRepository>();
builder.Services.AddTransient<StatusRepository>();
builder.Services.AddTransient<UnitOfWork>();
//builder.Services.AddSingleton<SectorRepository>();
//builder.Services.AddScoped<SectorRepository>();


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




var automapperLicence = builder.Configuration.GetSection("KeysConfigurations:AutomapperLicenceKey").Value;
//var automapperLicence2 = builder.Configuration.GetSection("AutomapperLicenceKey").Value;
//builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddAutoMapper(cfg => cfg.LicenseKey = automapperLicence, typeof(MappingProfile));


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<ResponseWrappingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
