using Application.Business;
using Application.Dtos;
using Domain.Entities;

namespace Application.Abstractions;

public interface IGroupService
{
    Task<SimpleMessageDto> CreateGroup(CreateGroupDto createGroupDto);
    List<GroupsDto> GetUserGroups();
}