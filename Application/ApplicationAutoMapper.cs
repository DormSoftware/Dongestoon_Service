using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application;

public class ApplicationAutoMapper : Profile
{
    protected ApplicationAutoMapper()
    {
        CreateMap<Group, GroupsDto>();
    }
}