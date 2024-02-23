using System.Text.RegularExpressions;
using Domain.Entities.UserEntity;

namespace Infrastructure.Abstractions;

public interface IUsersRepository
{
    User GetUserByIdIncludeEveryThing(Guid id);
    Task<User> GetUserByUserName(string UserName);
    public Task<User?> GetUserByUserNameIncludeGroups(string UserName);
    Task<List<User>> GetUsersByUserNames(List<string> userNames);
    User GetUserById(Guid id);
    List<User> GetUserByIdInRange(List<Guid> id);
}