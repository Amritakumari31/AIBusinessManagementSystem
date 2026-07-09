using Backend.API.Requests;
using Backend.API.Responses;
using Backend.API.Services;

using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtTokenService _jwtTokenService;

    public AuthController(JwtTokenService jwtTokenService)
    {
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        // Temporary hardcoded credentials
        if (request.Username != "admin" || request.Password != "admin123")
        {
            return Unauthorized("Invalid username or password.");
        }

        var token = _jwtTokenService.GenerateToken(request.Username);

        return Ok(new TokenResponse
        {
            Token = token
        });
    }
}
