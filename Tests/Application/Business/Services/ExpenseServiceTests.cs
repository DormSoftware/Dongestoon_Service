using Application.Abstractions;
using Application.Business.Services;
using Domain.Entities;
using Infrastructure.Abstractions;
using NSubstitute;
using Xunit;

namespace Tests.Application.Business.Services;

public class ExpenseServiceTests
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IExpenseService _sut;

    public ExpenseServiceTests()
    {
        _expenseRepository = Substitute.For<IExpenseRepository>();
        _sut = new ExpenseService(_expenseRepository);
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
            Title = "some title",
            Date = DateTime.Now,
        };

        _expenseRepository.CreateExpense(Arg.Is<CreateExpenseArg>(c => c.Equals(createExpenseArg))).Returns(expense);

        // Act
        var actual = _sut.CreateExpense(createExpenseArg);

        // Assert
    }
}