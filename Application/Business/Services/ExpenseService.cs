using Application.Abstractions;
using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Abstractions;

namespace Application.Business.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserStateHolder _currentUserStateHolder;

    public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper, ICurrentUserStateHolder currentUserStateHolder)
    {
        _expenseRepository = expenseRepository ?? throw new ArgumentNullException(nameof(expenseRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _currentUserStateHolder = currentUserStateHolder ?? throw new ArgumentNullException(nameof(currentUserStateHolder));
    }

    public ExpenseDto CreateExpense(CreateExpenseArg createExpenseArg)
    {
        createExpenseArg.UserId ??= _currentUserStateHolder.GetCurrentUser().Id;

        var expense = _expenseRepository.CreateExpense(createExpenseArg);
        return _mapper.Map<Expense, ExpenseDto>(expense);
    }
}