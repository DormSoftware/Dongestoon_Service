using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Abstractions;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Business;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public AuthService(ApplicationDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }


    public async Task<User> Login(string userName, string password)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Username.Equals(userName));
        return await Login(user, password);
    }

    public async Task<User> Login(Guid id, string password)
    {
        User? user = await _dbContext.Users.FindAsync(id);

        if (user == null || BCrypt.Net.BCrypt.Verify(password, user.Password) == false)
        {
            return null; //returning null intentionally to show that login was unsuccessful
        }

        return await Login(user, password);
    }

    public async Task<User> Login(User user, string password)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.GivenName, user.Name),
                new(ClaimTypes.Actor, user.Rank.ToString()),
            }),
            IssuedAt = DateTime.UtcNow,
            Issuer = _configuration["JWT:Issuer"],
            Audience = _configuration["JWT:Audience"],
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        user.Token = tokenHandler.WriteToken(token);
        user.IsActive = true;

        return user;
    }

    public async Task<User> Register(User user)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.IsActive = true;
        user.Rank = Rank.STARTER;
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }
}