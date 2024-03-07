using Application.Abstractions;
using Application.Business;
using Application.Business.Services;
using Application.Dtos;
using Domain.Entities;
using Infrastructure.Abstractions;
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

    [HttpPost]
    public ActionResult<List<Group>> CreateGroup([FromBody] CreateGroupDto createGroupDto)
    {
        _groupService.CreateGroup(createGroupDto);
        return Ok("Group created");
    }
}