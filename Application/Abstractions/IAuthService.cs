using Domain.Entities;
using Domain.Entities.UserEntity;

namespace Application.Abstractions;

public interface IAuthService
{
    Task<string> Login(string userName, string password);
    Task<User> Register(User user);
}