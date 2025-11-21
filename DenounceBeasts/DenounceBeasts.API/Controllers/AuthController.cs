// DenounceBeasts.Presentation/Controllers/v1/AuthController.cs
using DenounceBeasts.Business.Dtos;
using DenounceBeasts.Business.Responses;
using DenounceBeasts.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DenounceBeasts.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController : ControllerBase
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
    public async Task<IActionResult> Register([FromBody] RegisterRequest body, CancellationToken ct)
    {
        var token = await _auth.RegisterAsync(body.Email, body.Password, body.FullName, ct);
        var minutes = int.Parse(_cfg["Jwt:ExpireMinutes"]!);

        var payload = new TokenResponse { AccessToken = token, ExpiresIn = minutes * 60 };
        return Ok(ApiResponse<TokenResponse>.Success(payload,200, "Register OK"));
    }

     
}
