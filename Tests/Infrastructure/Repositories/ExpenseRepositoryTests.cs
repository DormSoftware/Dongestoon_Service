using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using FluentAssertions;
using Infrastructure.Abstractions;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Xunit;

namespace Tests.Infrastructure.Repositories;

public class ExpenseRepositoryTests
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IExpenseRepository _expenseRepository;
    private readonly IGroupRepository _groupRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public ExpenseRepositoryTests()
    {
        var mapperConfiguration = new MapperConfiguration(x => x.CreateMap<CreateExpenseArg, Expense>());

        _mapper = mapperConfiguration.CreateMapper();

        _usersRepository = Substitute.For<IUsersRepository>();
        _groupRepository = Substitute.For<IGroupRepository>();
        var dbContextOption = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("inMemoryDb");
        _applicationDbContext = new ApplicationDbContext(dbContextOption.Options);

        _expenseRepository = new ExpenseRepository(_applicationDbContext, _mapper, _usersRepository, _groupRepository);
    }

    [Fact]
    public void GenerateExpense_SHOULD_GenerateExpenseAndSaveItToDb_WHEN_GroupIdAndUserIdIsExistsInDb()
    {
        // Arrange
        var createExpenseArg = new CreateExpenseArg
        {
            Amount = 10,
            Description = "some description",
            Title = "some title",
            GroupId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };
        _groupRepository.Exists(Arg.Is<Guid>(x => x.Equals(createExpenseArg.GroupId))).Returns(true);
        _usersRepository.Exists(Arg.Is<Guid>(x => x.Equals(createExpenseArg.UserId))).Returns(true);

        // Act
        _expenseRepository.CreateExpense(createExpenseArg);
        var action = () =>
        {
            _applicationDbContext.Expense
                .Single(x => x.Description.Equals(createExpenseArg.Description) ||
                             x.Title.Equals(createExpenseArg.Title) ||
                             x.GroupId.Equals(createExpenseArg.GroupId) ||
                             x.UserId.Equals(createExpenseArg.UserId));
        };

        // Assert
        action.Should().NotThrow();
    }

    [Fact]
    public void GenerateExpense_SHOULD_ThrowNoUserFoundWithGivenIdException_WHEN_UserIdIsNotExistsInDb()
    {
        // Arrange
        var createExpenseArg = new CreateExpenseArg
        {
            Amount = 10,
            Description = "some description",
            Title = "some title",
            GroupId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };
        _groupRepository.Exists(Arg.Is<Guid>(x => x.Equals(createExpenseArg.GroupId))).Returns(true);
        _usersRepository.Exists(Arg.Is<Guid>(x => x.Equals(createExpenseArg.UserId))).Returns(false);

        // Act
        var action = () => _expenseRepository.CreateExpense(createExpenseArg);

        // Assert
        action.Should().Throw<AggregateException>().Which.InnerExceptions.Should().Contain(x => typeof(NoUserFoundWithGivenIdException).Equals(x.GetType()));
    }


    [Fact]
    public void GenerateExpense_SHOULD_ThrowNoGroupFoundWithGivenIdException_WHEN_GroupIdIsNotExistsInDb()
    {
        // Arrange
        var createExpenseArg = new CreateExpenseArg
        {
            Amount = 10,
            Description = "some description",
            Title = "some title",
            GroupId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };
        _groupRepository.Exists(Arg.Is<Guid>(x => x.Equals(createExpenseArg.GroupId))).Returns(false);
        _usersRepository.Exists(Arg.Is<Guid>(x => x.Equals(createExpenseArg.UserId))).Returns(true);

        // Act
        var action = () => _expenseRepository.CreateExpense(createExpenseArg);

        // Assert
        action.Should().Throw<AggregateException>().Which.InnerExceptions.Should().Contain(x => typeof(NoGroupFoundWithGivenIdException).Equals(x.GetType()));
    }
}