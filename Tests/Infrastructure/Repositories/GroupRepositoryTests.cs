using Domain.Entities;
using Domain.Entities.UserEntity;
using Domain.Exceptions;
using FluentAssertions;
using Infrastructure.Abstractions;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Tests.Infrastructure.Repositories;

public class GroupRepositoryTests
{
    private readonly IGroupRepository _sut;
    private ApplicationDbContext _dbContext;


    public GroupRepositoryTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>();
        dbContextOptions.UseInMemoryDatabase($"testDb");


        _dbContext = new ApplicationDbContext(dbContextOptions.Options);
        _sut = new GroupRepository(_dbContext);
    }

    [Fact]
    public void FindProductById_ShouldReturnProductWithGivenId_WhenEver()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User()
        {
            Id = userId,
            Username = "some user Name",
            Name = "some name",
            LastName = "some last name",
            Email = "some email",
            Password = "some pass"
        };
        _dbContext.Users.Add(user);
        var group = new Group
        {
            Id = Guid.NewGuid(),
            Name = "some pr name",
            OwnerId = userId,
            Users = new List<User>
            {
                user
            }
        };

        _dbContext.Groups.Add(group);
        _dbContext.SaveChanges();

        // Act
        var actual = _sut.GetAllUserGroups(userId);
        _dbContext.Groups.Remove(group);
        _dbContext.Users.Remove(user);
        _dbContext.SaveChanges();

        // Assert
        actual.Should().BeEquivalentTo(new List<Group>() { group });
    }

    [Fact]
    public void GenerateNewGroup_ShouldCreateNewGroupAndAddItToDb_WhenEver()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User()
        {
            Id = userId,
            Username = "some user Name",
            Name = "some name",
            LastName = "some last name",
            Email = "some email",
            Password = "some pass"
        };
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        // Act
        _sut.GenerateNewGroup(new CreateGroupParams
        {
            Name = "some group name",
            OwnerId = userId,
            Users = new List<User>() { user }
        });
        var actual = _sut.GetAllUserGroups(userId);

        _dbContext.Groups.Remove(_dbContext.Groups.First());
        _dbContext.Users.Remove(user);
        _dbContext.SaveChanges();

        // Assert
        actual.Count.Should().Be(1);
    }

    [Fact]
    public void GenerateNewGroup_ShouldThrowException_WhenOwnerUserIsNotPresentInUsersList()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User()
        {
            Id = userId,
            Username = "some user Name",
            Name = "some name",
            LastName = "some last name",
            Email = "some email",
            Password = "some pass"
        };
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        // Act
        var action = () => _sut.GenerateNewGroup(new CreateGroupParams
        {
            Name = "some group name",
            OwnerId = userId,
            Users = new List<User>()
        });

        _dbContext.Users.Remove(user);
        _dbContext.SaveChanges();

        // Assert
        action.Should().Throw<OwnerUserNotPresentInUsersListException>();
    }


    [Fact]
    public void GetGroupById_SHOULD_returnGroupWithGivenId_WHEN_ever()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var group = new Group()
        {
            Id = groupId,
            Name = "some group name",
            OwnerId = Guid.NewGuid()
        };
        _dbContext.Groups.Add(group);
        _dbContext.SaveChanges();

        // Assert
        var actual = _sut.GetGroupById(groupId);

        // Act
        actual.Should().BeEquivalentTo(group, options => options
            .IncludingNestedObjects()
            .AllowingInfiniteRecursion());
    }

    [Fact]
    public void GetGroupById_SHOULD_throwInvalidGroupIdException_WHEN_groupWithGivenIdCannotBeFound()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var group = new Group()
        {
            Id = groupId,
            Name = "some group name",
            OwnerId = Guid.NewGuid()
        };
        _dbContext.Groups.Add(group);
        _dbContext.SaveChanges();

        // Assert
        var actual = _sut.GetGroupById(groupId);

        // Act
        actual.Should().BeEquivalentTo(group, options => options
            .IncludingNestedObjects()
            .AllowingInfiniteRecursion());
    }


    [Fact]
    public void Exists_SHOULD_ReturnTrue_WHEN_AGroupWithGivenIdIsExistsInDb()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var group = new Group()
        {
            Id = groupId,
            Name = "some group name",
            OwnerId = Guid.NewGuid()
        };
        _dbContext.Groups.Add(group);
        _dbContext.SaveChanges();

        // Assert
        var actual = _sut.Exists(groupId);
        _dbContext.Groups.Remove(group);
        _dbContext.SaveChanges();

        // Act
        actual.Should().BeTrue();
    }


    [Fact]
    public void Exists_SHOULD_ReturnFalse_WHEN_AGroupWithGivenIdIsNotExistsInDb()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var group = new Group()
        {
            Id = groupId,
            Name = "some group name",
            OwnerId = Guid.NewGuid()
        };
        _dbContext.Groups.Add(group);
        _dbContext.SaveChanges();

        // Assert
        var actual = _sut.Exists(Guid.NewGuid());
        _dbContext.Groups.Remove(group);
        _dbContext.SaveChanges();

        // Act
        actual.Should().BeFalse();
    }
}