using Application.Dtos;

namespace Application.Abstractions;

public interface IGroupService
{
    Task<SimpleMessageDto> CreateGroup(CreateGroupDto createGroupDto);
    GroupsDto GetGroupById(Guid groupId);
    List<GroupsDto> GetUserGroups();
}