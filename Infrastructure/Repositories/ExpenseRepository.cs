using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Abstractions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ExpenseRepository : IExpenseRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;
    private readonly IUsersRepository _usersRepository;
    private readonly IGroupRepository _groupRepository;

    public ExpenseRepository(ApplicationDbContext applicationDbContext, IMapper mapper, IUsersRepository usersRepository, IGroupRepository groupRepository)
    {
        _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
        _groupRepository = groupRepository ?? throw new ArgumentNullException(nameof(groupRepository));
    }

    public Expense CreateExpense(CreateExpenseArg createExpenseArg)
    {
        var createExpenseExceptions = new List<Exception>();


        if (!_usersRepository.Exists((Guid) createExpenseArg.UserId))
        {
            createExpenseExceptions.Add(new NoUserFoundWithGivenIdException());
        }


        if (!_groupRepository.Exists(createExpenseArg.GroupId))
        {
            createExpenseExceptions.Add(new NoGroupFoundWithGivenIdException());
        }

        if (createExpenseExceptions.Any())
        {
            throw new AggregateException(createExpenseExceptions);
        }


        var expense = _mapper.Map<CreateExpenseArg, Expense>(createExpenseArg);

        var createdExpenseWithoutUserAndGroup = _applicationDbContext.Expense.Add(expense);

        _applicationDbContext.SaveChanges();

        var finalExpense = _applicationDbContext.Expense
            .Include(x => x.Group)
            .Include(x => x.User)
            .Single(x => x.Id.Equals(createdExpenseWithoutUserAndGroup.Entity.Id));

        return finalExpense;
    }
}