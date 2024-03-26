using Application.Dtos;
using Domain.Entities.UserEntity;

namespace Application.Abstractions;

public interface IGroupService
{
    Task<SimpleMessageDto> CreateGroupAsync(CreateGroupDto createGroupDto);
    GroupsDto GetGroupById(Guid groupId);
    List<GroupsDto> GetUserGroups();
    Task<IEnumerable<User>> AddGroupMemberAsync(AddGroupMemberRequest addGroupMemberRequest);

}