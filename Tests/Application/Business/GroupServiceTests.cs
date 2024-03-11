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
    // private readonly IGroupService _sut;
    // private readonly IGroupRepository _groupRepository;
    //
    // public GroupServiceTests()
    // {
    //     _groupRepository = Substitute.For<IGroupRepository>();
    //     _sut = new GroupService(_groupRepository);
    // }
    // //
    // [Fact]
    // public void CreateGroup_ShouldPassRightArgumentIntoGroupRepository_WhenEver()
    // {
    //     // Arrange
    //     var ownerId = Guid.NewGuid();
    //     var createGroupDto = new CreateGroupDto()
    //     {
    //         Name = "some group name",
    //         OwnerId = ownerId,
    //         Users = new List<Guid>
    //         {
    //             ownerId
    //         }
    //     };
    //
    //     // Act
    //     _sut.CreatGroup(createGroupDto);
    //
    //     // Assert
    //     _groupRepository.Received().GenerateNewGroup(Arg.Is<CreateGroupParams>(x => { }));
    // }
}