using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.UserEntity;
using Infrastructure.Abstractions;

namespace Application;

public class ApplicationAutoMapper : Profile
{
    public ApplicationAutoMapper()
    {
        CreateMap<Group, GroupsDto>().ForMember(x => x.Users, expression => expression.MapFrom(m => m.Users.Select(user => user.Id)));
        CreateMap<CreateExpenseArg, Expense>();
        CreateMap<User, UserDto>();
    }
}