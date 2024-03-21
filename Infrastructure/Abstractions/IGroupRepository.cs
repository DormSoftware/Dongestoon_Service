using Domain.Entities;
using Domain.Entities.UserEntity;

namespace Infrastructure.Abstractions;

public interface IGroupRepository
{
    ICollection<Group> GetAllUserGroups(Guid userId);
    Group GetGroupById(Guid groupId);
    public Group GenerateNewGroup(CreateGroupParams createGroupParams);

    public bool Exists(Guid id);
}

public struct CreateGroupParams
{
    public required Guid OwnerId { get; set; }
    public required string Name { get; set; }
    public required List<User> Users { get; set; }
}