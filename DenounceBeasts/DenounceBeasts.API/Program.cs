using DenounceBeasts.API.Controllers;
using DenounceBeasts.API.Middleware;
using DenounceBeasts.Application;
using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infrasctructure.Context;
using DenounceBeasts.Infrasctructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//builder.Services.AddSingleton<SectorRepository>();
//builder.Services.AddTransient<SectorRepository>();
//builder.Services.AddScoped<SectorRepository>();
//builder.Services.AddScoped<ComplaintTypeRepository>();

//Repositories
builder.Services.AddScoped<GenericRepository<ComplaintType>>();
builder.Services.AddScoped<MunicipalityRepository>();
builder.Services.AddScoped<SectorRepository>();
builder.Services.AddScoped<StatusRepository>();
builder.Services.AddScoped<UnitOfWork>();

//Services
builder.Services.AddScoped<SectorService>();
  
builder.Services.AddControllers();
builder.Services.AddOpenApi();
 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
   
//var automapperLicence = "ajsdhkajshdkjahskdjhasjkdhakjshdkjahsdkjahskjdhajkshdjkahjkshdjkasdhajkshdjkahsdkj";
var automapperLicence = builder.Configuration.GetSection("KeysConfigurations:AutomapperLicenceKey").Value;
//var automapperLicence2 = builder.Configuration.GetSection("AutomapperLicenceKey").Value;
//builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddAutoMapper(cfg => cfg.LicenseKey = automapperLicence, typeof(MappingProfile));



var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
//app.UseMiddleware<ResponseWrappingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
