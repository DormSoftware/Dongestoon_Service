using Application.Dtos;
using Infrastructure.Abstractions;

namespace Application.Abstractions;

public interface IExpenseService
{
    ExpenseDto CreateExpense(CreateExpenseArg createExpenseArg);
}