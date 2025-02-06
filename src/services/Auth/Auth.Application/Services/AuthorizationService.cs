using Egeshka.Auth.Application.Models;
using Egeshka.Auth.Application.Services.Interfaces;
using Egeshka.Auth.Application.Services.TokenProviders.Interfaces;
using Egeshka.Auth.Domain.ValueObjects;

namespace Egeshka.Auth.Application.Services;

public sealed class AuthorizationService(
    IAccessTokenProvider accessTokenGenerator,
    IRefreshTokenProvider refreshTokenGenerator)
    : IAuthorizationService
{
    public Task<AuthorizationData> GenerateAuthorizationDataAsync(UserId userId, CancellationToken cancellationToken)
    {
        var accessToken = accessTokenGenerator.GenerateToken(userId);
        var refreshToken = refreshTokenGenerator.GenerateToken(userId);
        var data = new AuthorizationData(accessToken, refreshToken);

        return Task.FromResult(data);
    }
}
