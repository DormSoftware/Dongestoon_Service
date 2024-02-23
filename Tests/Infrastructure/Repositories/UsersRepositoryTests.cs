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

public class UsersRepositoryTests
{
    private readonly IUsersRepository _sut;
    private ApplicationDbContext _dbContext;


    public UsersRepositoryTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>();
        dbContextOptions.UseInMemoryDatabase($"testDb");


        _dbContext = new ApplicationDbContext(dbContextOptions.Options);
        _sut = new UsersRepository(_dbContext);
    }

    [Fact]
    public void GetUserById_ShouldReturnUserWithGivenId_WhenEver()
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
        var actual = _sut.GetUserById(userId);
        _dbContext.Users.Remove(user);
        _dbContext.SaveChanges();

        // Assert
        actual.Should().BeEquivalentTo(user);
    }
}