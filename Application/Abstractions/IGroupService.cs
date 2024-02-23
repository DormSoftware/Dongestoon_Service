using Application.Business;
using Application.Dtos;
using Domain.Entities;

namespace Application.Abstractions;

public interface IGroupService
{
    Task CreatGroup(CreateGroupDto createGroupDto);
    List<GroupsDto> GetUserGroups();
}