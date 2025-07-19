using Microsoft.AspNetCore.Http.HttpResults;
using NArchitecture.Core.Mediator.Abstractions;
using NArchitecture.Starter.Application.Features.Auth.Commands.Login;

namespace NArchitecture.Starter.WebApi.Features.Auth.Endpoints;

/// <summary>
/// Configures the login endpoint
/// </summary>
public static class LoginEndpoint
{
    public static RouteGroupBuilder MapLoginEndpoint(this RouteGroupBuilder group)
    {
        _ = group
            .MapPost("/login", Handle)
            .WithName("Login")
            .WithOpenApi(operation =>
            {
                operation.Summary = "Authenticate a user";
                operation.Description =
                    "Authenticates a user with their email and password, optionally using two-factor authentication";
                return operation;
            });

        return group;
    }

    private record struct LoginRequest(string Email, string Password, string? AuthenticatorCode);

    private static async Task<Results<Ok<LoggedResponse>, BadRequest<string>>> Handle(
        LoginRequest request,
        IHttpContextAccessor httpContextAccessor,
        IMediator mediator
    )
    {
        string clientIp = httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;

        LoginCommand command = new(request.Email, request.Password, request.AuthenticatorCode, clientIp);
        LoggedResponse response = await mediator.SendAsync(command);

        return TypedResults.Ok(response);
    }
}
