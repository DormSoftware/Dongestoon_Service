using Application.Abstractions;
using Application.Dtos;
using Infrastructure.Abstractions;

namespace Application.Business.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;

    public ExpenseService(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository ?? throw new ArgumentNullException(nameof(expenseRepository));
    }

    public ExpenseDto CreateExpense(CreateExpenseArg createExpenseArg)
    {
        throw new NotImplementedException();
    }
}