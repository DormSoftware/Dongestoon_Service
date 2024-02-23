using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Business.RequestStates;
using Infrastructure.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Application.Business.Middlewares;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;


    public AuthMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task InvokeAsync(HttpContext context, IUsersRepository usersRepository,
        ICurrentUserStateHolder currentUserStateHolder)
    {
        string authHeader = context.Request.Headers["Authorization"];

        if (authHeader != null)
        {
            if (authHeader.StartsWith("Bearer"))
            {
                authHeader = authHeader.Substring("Bearer ".Length).Trim();
            }

            var handler = new JwtSecurityTokenHandler();

            JwtSecurityToken jwt = handler.ReadJwtToken(authHeader);

            var claims = new Dictionary<string, string>();

            var identity = new ClaimsIdentity(jwt.Claims, "basic");
            context.User = new ClaimsPrincipal(identity);

            foreach (var jwtClaim in jwt.Claims)
            {
                Console.WriteLine(jwtClaim.Type);
            }

            var userName = jwt.Claims.First();

            var user = await usersRepository.GetUserByUserNameIncludeGroups(userName.Value);
            currentUserStateHolder.SetUser(user);
        }

        await _next(context);
    }
}

public class GivenTokenIsNotValidException : Exception
{
}