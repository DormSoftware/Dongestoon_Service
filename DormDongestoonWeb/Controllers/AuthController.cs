using System.IdentityModel.Tokens.Jwt;
using Application.Abstractions;
using Application.Business.RequestStates;
using Domain.Entities;
using Domain.Entities.UserEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DormDongestoonWeb.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ICurrentUserStateHolder _currentUserStateHolder;

    public AuthController(IAuthService authService, ICurrentUserStateHolder currentUserStateHolder)
    {
        _authService = authService;
        _currentUserStateHolder =
            currentUserStateHolder ?? throw new ArgumentNullException(nameof(currentUserStateHolder));
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginUser user)
    {
        if (string.IsNullOrEmpty(user.UserName))
        {
            return BadRequest(new { message = "Username required" });
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            return BadRequest(new { message = "Password required" });
        }

        return Ok(await _authService.Login(user.UserName, user.Password));
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterUser user)
    {
        if (String.IsNullOrEmpty(user.Name))
        {
            return BadRequest(new { message = "Name required" });
        }

        if (String.IsNullOrEmpty(user.Username))
        {
            return BadRequest(new { message = "Username required" });
        }

        if (String.IsNullOrEmpty(user.Password))
        {
            return BadRequest(new { message = "Password required" });
        }

        User userToRegister = new(user.Username, user.Name, user.LastName, user.Email, user.Password);

        User registeredUser = await _authService.Register(userToRegister);

        var token = await _authService.Login(registeredUser.Username, user.Password);

        return Ok(token);
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

        return Ok(_currentUserStateHolder.GetCurrentUser());
    }
}