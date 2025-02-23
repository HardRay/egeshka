using Egeshka.Core.Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Egeshka.ApiGateway.Controllers;

[Authorize]
[ApiController]
public abstract class ControllerBaseWithIdentity : ControllerBase
{
    protected UserId GetUserIdOrThrow()
    {
        var subject = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
            ?? throw new InvalidDataException("No user information found in the authorization token");

        return long.TryParse(subject, out var userId)
            ? new UserId(userId)
            : throw new InvalidDataException("Invalid userId in the authorization token");

    }
}
