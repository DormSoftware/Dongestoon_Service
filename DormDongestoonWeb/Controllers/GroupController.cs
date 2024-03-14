using Application.Abstractions;
using Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DormDongestoonWeb.Controllers;

[Route("Groups")]
[Authorize]
public class GroupController : Controller
{
    private readonly IGroupService _groupService;

    public GroupController(IGroupService groupService)
    {
        _groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
    }


    [HttpGet]
    public ActionResult<List<GroupsDto>> GetUserGroup()
    {
        return _groupService.GetUserGroups();
    }

    [HttpGet]
    public ActionResult<GroupsDto> GetUserGroup([FromQuery] Guid id)
    {
        return _groupService.GetGroupById(id);
    }

    [HttpPost]
    public Task<SimpleMessageDto> CreateGroup([FromBody] CreateGroupDto createGroupDto)
    {
        return _groupService.CreateGroup(createGroupDto);
    }
}