using DenounceBeasts.API.Middleware;
using DenounceBeasts.API.Models;
using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infrasctructure.Data;
using DenounceBeasts.Infrasctructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//builder.Services.AddTransient<ComplaintTypeRepository>();
builder.Services.AddTransient<GenericRespository<ComplaintType>>();
builder.Services.AddTransient<MunicipalityRepository>();
builder.Services.AddTransient<SectorRepository>();
builder.Services.AddTransient<StatusRepository>();
builder.Services.AddTransient<UnitOfWork>();

//builder.Services.AddSingleton<StatusRepository>();
//builder.Services.AddScoped<StatusRepository>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var automapperLicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzkyMTk1MjAwIiwiaWF0IjoiMTc2MDY2MDQ4NyIsImFjY291bnRfaWQiOiIwMTk5ZTU1YTJkODM3Yzc1OGYzYTczNmU3ZTIyNDg3MSIsImN1c3RvbWVyX2lkIjoiY3RtXzAxazdqbm5lNjVmY2M1OHFwdDJ5OWFtdzVuIiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.AemkiHBRlSl8oUbWHkd6N9LeE41Nj7WXkNHRfNzIC0ETSm3MJa44yjsKhpGPMEsWnxJ3IPjO3GME_KrEXNYVgH7nki7Vde0KMzYd4kT4MI2kFaCLwrsh5Lkk3z-UCEF1q0MLneR-aQ7n-ua04SNmciEo5b_MF1vA9Tq8TqT5eqRBja58TWRABytXio-Qnu8_PZdMVkA8-hBIoSuYOoUpKKUC1a8mzHT87xnJscgenpjilUHLkuNF9OGDtnX069y7lpkCR2gxh_SQ8sfbX2W12JIy535682A-ZrcAYRt2q3jYT_MD_f-wMtw6c-qUjUy7cgtljVZkQxPyV5enqOpdLA";
var automapperLicence = builder.Configuration.GetSection("KeysConfigurations:AutomapperLicenceKey").Value;
//var automapperLicence2 = builder.Configuration.GetSection("AutomapperLicenceKey").Value;
//builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddAutoMapper(cfg => cfg.LicenseKey = automapperLicence, typeof(MappingProfile));





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
//app.UseMiddleware<ResponseWrappingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
