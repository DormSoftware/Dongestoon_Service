using Application.Abstractions;
using Application.Business.RequestStates;
using Application.Dtos;
using AutoMapper;
using Domain.Entities.UserEntity;
using Infrastructure.Abstractions;

namespace Application.Business.Services;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly ICurrentUserStateHolder _currentUserStateHolder;
    private readonly IMapper _mapper;

    public GroupService(IGroupRepository groupRepository, IUsersRepository usersRepository,
        ICurrentUserStateHolder currentUserStateHolder, IMapper mapper)
    {
        _groupRepository = groupRepository ?? throw new ArgumentNullException(nameof(groupRepository));
        _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
        _currentUserStateHolder =
            currentUserStateHolder ?? throw new ArgumentNullException(nameof(currentUserStateHolder));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<SimpleMessageDto> CreateGroupAsync(CreateGroupDto createGroupDto)
    {
        var users = await _usersRepository.GetUsersByUserNames(createGroupDto.Users);

        _groupRepository.GenerateNewGroup(new CreateGroupParams
        {
            Name = createGroupDto.Name,
            OwnerId = _currentUserStateHolder.GetCurrentUser().Id,
            Users = users
        });

        return new SimpleMessageDto
        {
            Message = "Your group has been created!"
        };
    }

    public GroupsDto GetGroupById(Guid groupId)
    {
        return _mapper.Map<GroupsDto>(_groupRepository.GetGroupById(groupId));
    }

    public List<GroupsDto> GetUserGroups()
    {
        return _currentUserStateHolder
            .GetCurrentUser()
            .Groups.Select(item => _mapper.Map<GroupsDto>(item)).ToList();
    }

    public async Task<IEnumerable<User>> AddGroupMemberAsync(AddGroupMemberRequest addGroupMemberRequest)
    {
        var user = _usersRepository.GetUserById(addGroupMemberRequest.UserId);
        var group = await _groupRepository.AddGroupMemberAsync(addGroupMemberRequest.GroupId, user);

        return group.Users;
    }
}