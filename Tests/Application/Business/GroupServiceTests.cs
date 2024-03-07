using Application.Abstractions;
using Application.Business;
using Application.Business.RequestStates;
using Application.Business.Services;
using Application.Dtos;
using Domain.Entities;
using Domain.Entities.UserEntity;
using FluentAssertions;
using Infrastructure.Abstractions;
using NSubstitute;
using Xunit;

namespace Tests.Application.Business;

public class GroupServiceTests
{
    private readonly IGroupService sut;
    private readonly IUsersRepository usersRepository;
    private readonly IGroupRepository groupRepository;
    private readonly ICurrentUserStateHolder currentUserStateHolder;

    public GroupServiceTests()
    {
        usersRepository = Substitute.For<IUsersRepository>();
        groupRepository = Substitute.For<IGroupRepository>();
        currentUserStateHolder = Substitute.For<ICurrentUserStateHolder>();
        this.sut = new GroupService(groupRepository, usersRepository, currentUserStateHolder);
    }


    // [Fact]
    // public void GetUserGroups_SHOULD_returnListOfUserGroup()
    // {
    //     // Arrange
    //     var user = new User
    //     {
    //         Groups = [
    //             new Group{
    //                     Name = "some group name",
    //                     OwnerId = Guid.NewGuid(),
    //                     Users = []
    //                 }
    //         ]
    //     };

    //     var expected = new GroupsDto{
    //         Name = user.Groups[0].Name,
    //         OwnerId = user.Groups[0].OwnerId,
    //         Users = []
    //     };

    //     currentUserStateHolder.GetCurrentUser().Returns(user);

    //     // Act
    //     var actual = sut.GetUserGroups();

    //     // Assert
    //     actual[0].Should().BeEquivalentTo(expected,option => option.ExcludingNestedObjects());
    // }

    [Fact]
    public void CreateGroup_SHOULD_callGroupRepositoryGenerateNewGroup_WHEN_Ever()
    {
        // Arrange
        var user = new User
        {
            Groups = [
                new Group{
                        Name = "some group name",
                        OwnerId = Guid.NewGuid(),
                        Users = []
                    }
            ]
        };

        currentUserStateHolder.GetCurrentUser().Returns(user);

        var createGroupDto = new CreateGroupDto
        {
            Name = "some group name",
            Users = []
        };

        // Act
        var actual = sut.CreateGroup(createGroupDto);

        // Assert
        groupRepository.ReceivedCalls();
    }

}