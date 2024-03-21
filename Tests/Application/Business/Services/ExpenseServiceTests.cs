using Application.Abstractions;
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

public class ExpenseServiceTests
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly ICurrentUserStateHolder _currentUserStateHolder;
    private readonly IMapper _mapper;
    private readonly IExpenseService _sut;

    public ExpenseServiceTests()
    {
        _currentUserStateHolder = Substitute.For<ICurrentUserStateHolder>();
        _mapper = new MapperConfiguration(x =>
        {
            x.CreateMap<Expense, ExpenseDto>();
            x.CreateMap<User, UserDto>();
            x.CreateMap<Group, GroupsDto>();
        }).CreateMapper();
        _expenseRepository = Substitute.For<IExpenseRepository>();
        _sut = new ExpenseService(_expenseRepository, _mapper, _currentUserStateHolder);
    }

    [Fact]
    public void CreateExpense_SHOULD_ReplaceUserIdWithCurrentUserId_WhenUserIdNotProvided()
    {
        // Arrange
        var mockUser = new User { Id = Guid.NewGuid() };
        _currentUserStateHolder.GetCurrentUser().Returns(mockUser);
        var createExpenseArg = new CreateExpenseArg
        {
            Amount = 10,
            Description = "some description",
            GroupId = Guid.NewGuid(),
            Title = "some title"
        };

        var expense = new Expense
        {
            Id = Guid.NewGuid(),
            Amount = 10,
            Description = "some description",
            GroupId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Group = new Group(),
            User = new User(),
            Title = "some title",
            Date = DateTime.Now,
        };


        var expected = new ExpenseDto
        {
            Id = expense.Id,
            Amount = expense.Amount,
            Title = expense.Title,
            Description = expense.Description,
            Date = expense.Date
        };

        var createExpenseArgWithReplacedUserId = createExpenseArg with { UserId = mockUser.Id };
        _expenseRepository.CreateExpense(Arg.Is<CreateExpenseArg>(c => c.Equals(createExpenseArgWithReplacedUserId))).Returns(expense);

        // Act
        var actual = _sut.CreateExpense(createExpenseArg);

        // Assert
        actual.Should().BeEquivalentTo(expected, options =>
            options.ComparingByMembers<ExpenseDto>()
                .ComparingByMembers<UserDto>()
                .ComparingByMembers<GroupsDto>()
                .Excluding(x => x.Group.Users));
    }

    [Fact]
    public void CreateExpense_SHOULD_returnCreatedExpenseDto_WhenEver()
    {
        // Arrange
        var createExpenseArg = new CreateExpenseArg
        {
            Amount = 10,
            Description = "some description",
            GroupId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "some title"
        };

        var expense = new Expense
        {
            Id = Guid.NewGuid(),
            Amount = 10,
            Description = "some description",
            GroupId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Group = new Group(),
            User = new User(),
            Title = "some title",
            Date = DateTime.Now,
        };

        var expected = new ExpenseDto
        {
            Id = expense.Id,
            Amount = expense.Amount,
            Title = expense.Title,
            Description = expense.Description,
            Date = expense.Date
        };


        _expenseRepository.CreateExpense(Arg.Is<CreateExpenseArg>(c => c.Equals(createExpenseArg))).Returns(expense);

        // Act
        var actual = _sut.CreateExpense(createExpenseArg);

        // Assert
        actual.Should().BeEquivalentTo(expected, options =>
            options.ComparingByMembers<ExpenseDto>()
                .ComparingByMembers<UserDto>()
                .ComparingByMembers<GroupsDto>()
                .Excluding(x => x.Group.Users));
    }
}