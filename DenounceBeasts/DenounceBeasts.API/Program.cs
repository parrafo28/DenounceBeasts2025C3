using DenounceBeasts.API.Controllers;
using DenounceBeasts.Business.Profiles;
using DenounceBeasts.Business.Services;
using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infrasctructure.Data;
using DenounceBeasts.Infrasctructure.Repositories;
using DenounceBeasts.Presentation.Filters;
using DenounceBeasts.Presentation.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

builder.Services.AddTransient<SectorService>();

//builder.Services.AddSingleton<StatusRepository>();
//builder.Services.AddScoped<StatusRepository>();
builder.Services.AddScoped<AuthService>();

builder.Services
    .AddControllers(opp=> { 
    opp.Filters.Add<ValidateModelFilter>();
    });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var automapperLicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzkyMTk1MjAwIiwiaWF0IjoiMTc2MDY2MDQ4NyIsImFjY291bnRfaWQiOiIwMTk5ZTU1YTJkODM3Yzc1OGYzYTczNmU3ZTIyNDg3MSIsImN1c3RvbWVyX2lkIjoiY3RtXzAxazdqbm5lNjVmY2M1OHFwdDJ5OWFtdzVuIiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.AemkiHBRlSl8oUbWHkd6N9LeE41Nj7WXkNHRfNzIC0ETSm3MJa44yjsKhpGPMEsWnxJ3IPjO3GME_KrEXNYVgH7nki7Vde0KMzYd4kT4MI2kFaCLwrsh5Lkk3z-UCEF1q0MLneR-aQ7n-ua04SNmciEo5b_MF1vA9Tq8TqT5eqRBja58TWRABytXio-Qnu8_PZdMVkA8-hBIoSuYOoUpKKUC1a8mzHT87xnJscgenpjilUHLkuNF9OGDtnX069y7lpkCR2gxh_SQ8sfbX2W12JIy535682A-ZrcAYRt2q3jYT_MD_f-wMtw6c-qUjUy7cgtljVZkQxPyV5enqOpdLA";
var automapperLicence = builder.Configuration.GetSection("KeysConfigurations:AutomapperLicenceKey").Value;
//var automapperLicence2 = builder.Configuration.GetSection("AutomapperLicenceKey").Value;
//builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddAutoMapper(cfg => cfg.LicenseKey = automapperLicence, typeof(MappingProfile));


builder.Services.AddCors(o =>
{
    o.AddPolicy("frontend", p =>
    {
        var origins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
        p.WithOrigins(origins)
         .AllowAnyHeader()
         .AllowAnyMethod()
         .AllowAnyOrigin()
         //.AllowCredentials()
         ; // quítalo si usas AllowAnyOrigin
    });

    // Política abierta (solo dev puntual)
    o.AddPolicy("open-dev", p =>
        p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var jwt = builder.Configuration.GetSection("Jwt");
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            
            ValidateIssuer = true,
            ValidIssuer = jwt["Issuer"],
            ValidateAudience = true,
            ValidAudience = jwt["Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(2)
        };
    });

builder.Services.AddAuthorization();


var app = builder.Build();

app.UseCors("frontend");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("open-dev");
}

app.UseMiddleware<ExceptionMiddleware>();
//app.UseMiddleware<ResponseWrappingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
 

app.MapControllers();

app.Run();
