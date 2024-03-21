using Application.Abstractions;
using Application.Dtos;
using Infrastructure.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DormDongestoonWeb.Controllers;

[Route("Expenses")]
[Authorize]
public class ExpenseController : Controller
{
    private readonly IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService ?? throw new ArgumentNullException(nameof(expenseService));
    }

    [HttpPost]
    public ActionResult<ExpenseDto> AddExpense([FromBody] CreateExpenseArg createExpenseArg)
    {
        return _expenseService.CreateExpense(createExpenseArg);
    }
}