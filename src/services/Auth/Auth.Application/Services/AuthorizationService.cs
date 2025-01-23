using Egeshka.Auth.Application.Models;
using Egeshka.Auth.Application.Services.Interfaces;
using Egeshka.Auth.Application.Services.TokenGenerators.Interfaces;

namespace Egeshka.Auth.Application.Services;

public sealed class AuthorizationService(
    IAccessTokenGenerator accessTokenGenerator,
    IRefreshTokenGenerator refreshTokenGenerator)
    : IAuthorizationService
{
    public Task<AuthorizationData> GenerateAuthorizationDataAsync(long userId, CancellationToken cancellationToken)
    {
        var accessToken = accessTokenGenerator.GenerateToken(userId);
        var refreshToken = refreshTokenGenerator.GenerateToken(userId);
        var data = new AuthorizationData(accessToken, refreshToken);

        return Task.FromResult(data);
    }
}
