using Domain.Entities;

namespace Infrastructure.Abstractions;

public interface IExpenseRepository
{
    Expense CreateExpense(CreateExpenseArg createExpenseArg);
}

public struct CreateExpenseArg
{
    public Guid? UserId { get; set; }
    public Guid GroupId { get; set; }
    public decimal Amount { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}