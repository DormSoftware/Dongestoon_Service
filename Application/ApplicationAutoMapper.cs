using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Abstractions;

namespace Application;

public class ApplicationAutoMapper : Profile
{
    protected ApplicationAutoMapper()
    {
        CreateMap<Group, GroupsDto>();
        CreateMap<CreateExpenseArg, Expense>();
    }
}