using System.IdentityModel.Tokens.Jwt;
using Application.Abstractions;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DormDongestoonWeb.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginUser user)
    {
        if (String.IsNullOrEmpty(user.UserName))
        {
            return BadRequest(new { message = "Email address needs to entered" });
        }
        else if (String.IsNullOrEmpty(user.Password))
        {
            return BadRequest(new { message = "Password needs to entered" });
        }

        User loggedInUser = await _authService.Login(user.UserName, user.Password);

        if (loggedInUser != null)
        {
            return Ok(loggedInUser);
        }

        return BadRequest(new { message = "User login unsuccessful" });
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterUser user)
    {
        if (String.IsNullOrEmpty(user.Name))
        {
            return BadRequest(new { message = "Name needs to entered" });
        }
        else if (String.IsNullOrEmpty(user.Username))
        {
            return BadRequest(new { message = "User name needs to entered" });
        }
        else if (String.IsNullOrEmpty(user.Password))
        {
            return BadRequest(new { message = "Password needs to entered" });
        }

        User userToRegister = new(user.Username, user.Name, user.LastName, user.Email, user.Password);

        User registeredUser = await _authService.Register(userToRegister);

        User loggedInUser = await _authService.Login(registeredUser.Id, user.Password);

        if (loggedInUser != null)
        {
            return Ok(loggedInUser);
        }

        return BadRequest(new { message = "User registration unsuccessful" });
    }

    [HttpGet]
    public IActionResult Test()
    {
        string token = Request.Headers["Authorization"];

        if (token.StartsWith("Bearer"))
        {
            token = token.Substring("Bearer ".Length).Trim();
        }

        var handler = new JwtSecurityTokenHandler();

        JwtSecurityToken jwt = handler.ReadJwtToken(token);

        var claims = new Dictionary<string, string>();

        foreach (var claim in jwt.Claims)
        {
            claims.Add(claim.Type, claim.Value);
        }

        return Ok(claims);
    }
}