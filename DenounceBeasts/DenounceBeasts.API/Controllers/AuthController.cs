using DenounceBeasts.Application.Responses;
using DenounceBeasts.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace DenounceBeasts.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;
        private readonly IConfiguration _cfg;

        public AuthController(AuthService auth, IConfiguration cfg)
        {
            _auth = auth; _cfg = cfg;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest body, CancellationToken ct)
        {
            var token = await _auth.LoginAsync(body.Email, body.Password, ct);
            var minutes = int.Parse(_cfg["Jwt:ExpireMinutes"]!);

            var payload = new TokenResponse { AccessToken = token, ExpiresIn = minutes * 60 };
            return Ok(ApiResponse<TokenResponse>.Success(payload, 200, "Login OK"));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] DenounceBeasts.Application.Requests.RegisterRequest body, CancellationToken ct)
        {
            var token = await _auth.RegisterAsync(body.Email, body.Password, body.Name, body.LastName, ct);
            var minutes = int.Parse(_cfg["Jwt:ExpireMinutes"]!);

            var payload = new TokenResponse { AccessToken = token, ExpiresIn = minutes * 60 };
            return Ok(ApiResponse<TokenResponse>.Success(payload, 200, "Register OK"));
        }
    }
}
