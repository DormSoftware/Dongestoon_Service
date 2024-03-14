using Application.Abstractions;
using Application.Business.Middlewares;
using Application.Business.RequestStates;
using Application.Business.Services;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructureServices(configuration);
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IGroupService, GroupService>();
        services.AddScoped<ICurrentUserStateHolder, CurrentUserStateHolder>();
        services.AddAutoMapper(typeof(ApplicationAutoMapper));
        return services;
    }

    public static void AddApplicationsMiddlewares(this WebApplication application)
    {
        application.UseMiddleware<AuthMiddleware>();
    }
}