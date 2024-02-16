using Domain.Entities;

namespace Application.Abstractions;

public interface IAuthService
{
    public Task<User> Login(Guid id, string password);
    public Task<User> Login(string userName, string password);
    public Task<User> Register(User user);
}