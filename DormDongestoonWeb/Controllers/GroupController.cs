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

    [HttpGet("/{id}")]
    public ActionResult<GroupsDto> GetUserGroup(string id)
    {
        return _groupService.GetGroupById(Guid.Parse(id));
    }

    [HttpPost]
    public Task<SimpleMessageDto> CreateGroup([FromBody] CreateGroupDto createGroupDto)
    {
        return _groupService.CreateGroupAsync(createGroupDto);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddGroupMember([FromBody] AddGroupMemberRequest addGroupMemberRequest)
    {
        var response =await _groupService.AddGroupMemberAsync(addGroupMemberRequest);

        return Ok(response);
    }
}