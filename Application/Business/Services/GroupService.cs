using Application.Abstractions;
using Application.Business.RequestStates;
using Application.Dtos;
using Domain.Entities;
using Infrastructure.Abstractions;

namespace Application.Business.Services;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly ICurrentUserStateHolder _currentUserStateHolder;

    public GroupService(IGroupRepository groupRepository, IUsersRepository usersRepository,
        ICurrentUserStateHolder currentUserStateHolder)
    {
        _groupRepository = groupRepository ?? throw new ArgumentNullException(nameof(groupRepository));
        _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
        _currentUserStateHolder =
            currentUserStateHolder ?? throw new ArgumentNullException(nameof(currentUserStateHolder));
    }

    public async Task CreatGroup(CreateGroupDto createGroupDto)
    {
        var users = await _usersRepository.GetUsersByUserNames(createGroupDto.Users);

        _groupRepository.GenerateNewGroup(new CreateGroupParams
        {
            Name = createGroupDto.Name,
            OwnerId = _currentUserStateHolder.GetCurrentUser().Id,
            Users = users
        });
    }

    public List<GroupsDto> GetUserGroups()
    {
        return _currentUserStateHolder.GetCurrentUser().Groups.Select(item => new GroupsDto
        {
            Name = item.Name,
            OwnerId = item.OwnerId,
            Users = item.Users.Select(x => x.Id).ToList()
        }).ToList();
    }
}