using Application.Abstractions;
using Application.Business.RequestStates;
using Application.Business.Services;
using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.UserEntity;
using Domain.Enums;
using FluentAssertions;
using Infrastructure.Abstractions;
using NSubstitute;
using Xunit;

namespace Tests.Application.Business.Services;

public class GroupServicesTests
{
    private readonly ICurrentUserStateHolder _currentUserStateHolder;
    private readonly IGroupService _sut;
    private readonly IUsersRepository _usersRepository;
    private readonly IGroupRepository _groupRepository;

    public GroupServicesTests()
    {
        _currentUserStateHolder = Substitute.For<ICurrentUserStateHolder>();

        var mapper = new MapperConfiguration(x => x.CreateMap<Group, GroupsDto>());

        _groupRepository = Substitute.For<IGroupRepository>();
        _usersRepository = Substitute.For<IUsersRepository>();
        _sut = new GroupService(_groupRepository, _usersRepository, _currentUserStateHolder, mapper.CreateMapper());
    }

    [Fact]
    public void GetUserGroup_SHOULD_ReturnUserGroupDTO_WHEN_ever()
    {
        // Arrange
        var group = new Group
        {
            Id = Guid.NewGuid(),
            Name = "some gp name",
            Rank = GroupRank.STARTER,
            Users = new List<User>(),
            TotalCost = 0,
            ProfilePic = Guid.NewGuid(),
            OwnerId = Guid.NewGuid()
        };
        _currentUserStateHolder.GetCurrentUser().Returns(new User()
        {
            Groups = new List<Group>() { group }
        });

        var expected = new GroupsDto()
        {
            Name = group.Name,
            Rank = group.Rank,
            Users = new List<Guid>(),
            OwnerId = group.OwnerId,
            TotalCost = group.TotalCost,
            ProfilePic = group.ProfilePic
        };

        // Act
        var actual = _sut.GetUserGroups();

        // Assert
        actual[0].Should().BeEquivalentTo(expected, options => options.ComparingByMembers<GroupsDto>());
    }

    [Fact]
    public async Task AddGroupMemberAsync_WhenEver_ReturnsUsers()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "hossein",
            Username = "hhh",
            LastName = "Shirkavand",
            Email = "h@g.c"
        };
        var group = new Group
        {
            Id = Guid.NewGuid(),
            Name = "name",
            OwnerId = new Guid(),
            Users = [user]
        };
        _usersRepository.GetUserById(user.Id).Returns(user);
        _groupRepository.AddGroupMemberAsync(group.Id, user).Returns(group);
        var addGroupRequest = new AddGroupMemberRequest
        {
            GroupId = group.Id,
            UserId = user.Id
        };
        // Act
        var actual = await _sut.AddGroupMemberAsync(addGroupRequest);
        // Assert
        actual.Should().Contain(user);
    }
}