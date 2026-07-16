using Backend.API.Requests;
using Backend.API.Responses;
using Backend.Application.Interfaces.Services;
using System.Threading.Tasks;
using Backend.Domain.Entities;

using Microsoft.AspNetCore.Mvc;
using Azure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Backend.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IJwtTokenService _jwtTokenService;
    private readonly UserManager<User> _userManager;

    public AuthController(IJwtTokenService jwtTokenService,UserManager<User>userManager)
    {
        _jwtTokenService = jwtTokenService;
        _userManager = userManager;

    }

    [HttpPost("register")]
    public async Task<IActionResult>
        Register()
    {
        var existingUser = await _userManager.FindByNameAsync("admin");
        if (existingUser != null)
        {
            return Ok("Admin user already exists.");
        }

        var user = new User
        {
            UserName = "admin",
            Email = "admin@test.com",
            FullName = "Administrator"
        };
        var result = await _userManager.CreateAsync(user, "Admin@123");
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok("Admin user created successfully.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);

        if (user == null)
        {
            return Unauthorized("Invalid username or password.");
        }

        var validPassword = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!validPassword)
        {
            return Unauthorized("Invalid username or password.");
        }

        var token = await _jwtTokenService.GenerateTokenAsync(user);

        return Ok(new TokenResponse
        {
            Token = token
        });
    }
}


