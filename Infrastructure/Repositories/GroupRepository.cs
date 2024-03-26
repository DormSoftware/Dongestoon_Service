using Domain.Entities;
using Domain.Entities.UserEntity;
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

    public Group GetGroupById(Guid groupId)
    {
        return _dbContext.Groups.Include(x => x.Users).SingleOrDefault(x => x.Id.Equals(groupId)) ?? throw new InvalidGroupIdException($"there is no group with id : {groupId}");
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

    public async Task<Group> AddGroupMemberAsync(Guid groupId, User user)
    {
        var group = GetGroupById(groupId);
        
        group.Users.Add(user);
        await _dbContext.SaveChangesAsync();
        return group;
    }
}