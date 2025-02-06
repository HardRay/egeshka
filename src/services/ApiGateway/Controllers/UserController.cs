using Egeshka.ApiGateway.Dtos;
using Egeshka.ApiGateway.Dtos.UserLogin;
using Egeshka.ApiGateway.Dtos.UserRelogin;
using Egeshka.ApiGateway.Providers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Egeshka.ApiGateway.Controllers;

[ApiController]
[Route("/api/user")]
public class UserController(IAuthProvider authProvider) : ControllerBaseWithIdentity
{
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesDefaultResponseType]
    [ProducesResponseType<UserLoginResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorDto>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorDto>(StatusCodes.Status404NotFound)]
    public Task<UserLoginResponse> Login(UserLoginRequest request, CancellationToken cancellationToken)
    {
        return authProvider.LoginAsync(request, cancellationToken);
    }

    [HttpPost("relogin")]
    [AllowAnonymous]
    [ProducesDefaultResponseType]
    [ProducesResponseType<UserLoginResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorDto>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorDto>(StatusCodes.Status404NotFound)]
    public Task<UserReloginResponse> Relogin(UserReloginRequest request, CancellationToken cancellationToken)
    {
        return authProvider.ReloginAsync(request, cancellationToken);
    }

    [HttpGet("id")]
    public Task<long> GetMyId()
    {
        return Task.FromResult(GetUserIdOrThrow());
    }
}
