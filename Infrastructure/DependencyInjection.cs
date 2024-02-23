using Infrastructure.Abstractions;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(x => x.UseInMemoryDatabase("ApplicationDataBase"));
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        return services;
    }
}