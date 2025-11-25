using DenounceBeasts.API.Controllers;
using DenounceBeasts.Application;
using DenounceBeasts.Application.Services;
using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infrasctructure.Context;
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
builder.Services.AddScoped<AuthService>();

builder.Services
    .AddControllers(opp => {
        opp.Filters.Add<ValidateModelFilter>();
    });

builder.Services.AddOpenApi();
 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
   
//var automapperLicence = "ajsdhkajshdkjahskdjhasjkdhakjshdkjahsdkjahskjdhajkshdjkahjkshdjkasdhajkshdjkahsdkj";
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

app.UseMiddleware<ExceptionMiddleware>();
//app.UseMiddleware<ResponseWrappingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("open-dev");

    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
