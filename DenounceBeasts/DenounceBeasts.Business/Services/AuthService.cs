using BCrypt.Net;
using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infrasctructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DenounceBeasts.Business.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IConfiguration _cfg;

        public AuthService(ApplicationDbContext ctx, IConfiguration cfg)
        {
            _ctx = ctx; _cfg = cfg;
        }

        public async Task<string> RegisterAsync(string email, string password, string fullName, CancellationToken ct)
        {
            email = email.Trim().ToLowerInvariant();

            var exists = await _ctx.Users.AnyAsync(u => u.Email == email, ct);
            if (exists) throw new InvalidOperationException("Email already registered.");

            var user = new User
            {
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                FullName = fullName.Trim(),
                Role = "User",
                IsActive = true
            };

            await _ctx.Users.AddAsync(user, ct);
            await _ctx.SaveChangesAsync(ct);

            return GenerateJwt(user);
        }

        public async Task<string> LoginAsync(string email, string password, CancellationToken ct)
        {
            email = email.Trim().ToLowerInvariant();

            var user = await _ctx.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email, ct);
            if (user is null) throw new InvalidOperationException("Invalid credentials.");
            if (!user.IsActive) throw new InvalidOperationException("User is inactive.");

            var ok = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!ok) throw new InvalidOperationException("Invalid credentials.");

            return GenerateJwt(user);
        }

        private string GenerateJwt(User user)
        {
            var jwt = _cfg.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwt["ExpireMinutes"]!)),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
