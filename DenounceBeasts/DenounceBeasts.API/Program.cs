using DenounceBeasts.API.Data;
using DenounceBeasts.API.Middleware;
using DenounceBeasts.API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
