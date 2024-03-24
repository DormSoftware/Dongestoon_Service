using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Abstractions;
using Application.Dtos;
using Domain.Entities.UserEntity;
using Domain.Enums;
using Domain.Exceptions;
using Infrastructure.Abstractions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Business.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IUsersRepository _usersRepository;

    public AuthService(IConfiguration configuration, IUsersRepository usersRepository)
    {
        _configuration = configuration;
        _usersRepository = usersRepository;
    }

    public async Task<string> Login(string userName, string password)
    {
        var user = await _usersRepository.GetUserByUserName(userName);
        if (user is null)
        {
            throw new NoUserFoundWithGivenUserNameException(userName);
        }

        if (BCrypt.Net.BCrypt.Verify(password, user.Password) == false)
        {
            throw new PasswordDoesNotMatchWithUserNameException();
        }

        return TokenGenerator(new TokenGeneratorDto
        {
            Username = user.Username,
            Id = user.Id
        });
    }

    /*public async Task<User> Login(User user, string password)
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
    }*/

    public async Task<User> Register(User user)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.IsActive = true;
        user.Rank = Rank.STARTER;
        await _usersRepository.AddUserAsync(user);

        return user;
    }

    private string TokenGenerator(TokenGeneratorDto tokenGeneratorDto)
    {
        var key = Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]);
        var claims = new List<Claim>()
        {
            new(ClaimTypes.Name, tokenGeneratorDto.Username),
            new("id", tokenGeneratorDto.Id.ToString()),
        };

        var jwt = new JwtSecurityToken(claims: claims,
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            signingCredentials:
            new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        );
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}