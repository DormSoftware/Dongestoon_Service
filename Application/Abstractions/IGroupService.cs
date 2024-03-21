using Application.Dtos;

namespace Application.Abstractions;

public interface IGroupService
{
    Task<GroupsDto> CreateGroup(CreateGroupDto createGroupDto);
    GroupsDto GetGroupById(Guid groupId);
    List<GroupsDto> GetUserGroups();
}