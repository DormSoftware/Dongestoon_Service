using Domain.Entities.UserEntity;
using Domain.Exceptions;
using Infrastructure.Abstractions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UsersRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public User GetUserByIdIncludeEveryThing(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetUserByUserName(string UserName)
    {
        return _dbContext.Users.SingleOrDefaultAsync(user => user.Username.Equals(UserName)) ??
               throw new NoUserFoundWithGivenUserNameException(UserName);
    }

    public async Task<List<User>> GetUsersByUserNames(List<string> userNames)
    {
        var users = new List<User>();

        foreach (var userName in userNames)
        {
            users.Add(await GetUserByUserName(userName));
        }

        return users;
    }

    public Task<User?> GetUserByUserNameIncludeGroups(string UserName)
    {
        return _dbContext.Users.Include(x => x.Groups).SingleOrDefaultAsync(user => user.Username.Equals(UserName)) ??
               throw new NoUserFoundWithGivenUserNameException(UserName);
    }

    public User GetUserById(Guid id)
    {
        return _dbContext.Users.Find(id) ?? throw new NoUserFoundWithGivenIdException(id);
    }

    public List<User> GetUserByIdInRange(List<Guid> ids)
    {
        return ids.Select(id => _dbContext.Users.Find(id) ?? throw new NoUserFoundWithGivenIdException(id)).ToList();
    }

    public async Task<User> AddUserAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }
}