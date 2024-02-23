using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Abstractions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GroupRepository : IGroupRepository
{
    private readonly ApplicationDbContext _dbContext;

    public GroupRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public ICollection<Group> GetAllUserGroups(Guid userId)
    {
        var user = _dbContext.Users.Include(x => x.Groups).SingleOrDefault(x => x.Id.Equals(userId)) ??
                   throw new NoUserFoundWithGivenIdException(userId);
        return user.Groups;
    }

    public void GenerateNewGroup(CreateGroupParams createGroupParams)
    {
        if (!createGroupParams.Users.Any(x => x.Id.Equals(createGroupParams.OwnerId)))
        {
            throw new OwnerUserNotPresentInUsersListException(createGroupParams.OwnerId);
        }

        var newGroup = new Group
        {
            Name = createGroupParams.Name,
            OwnerId = createGroupParams.OwnerId,
            Users = createGroupParams.Users
        };

        _dbContext.Groups.Add(newGroup);
        _dbContext.SaveChanges();
    }
}