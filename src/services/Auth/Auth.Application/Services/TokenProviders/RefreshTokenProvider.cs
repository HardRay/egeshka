using Egeshka.Auth.Application.Services.TokenProviders.Interfaces;
using Egeshka.Core.Domain.ValueObjects;
using Microsoft.IdentityModel.Tokens;

namespace Egeshka.Auth.Application.Services.TokenProviders;

public sealed class RefreshTokenProvider : IRefreshTokenProvider
{
    public string GenerateToken(UserId userId)
    {
        var user = BitConverter.GetBytes(userId);
        var time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
        var key = Guid.NewGuid().ToByteArray();

        var bytes = user.Concat(time).Concat(key).ToArray();
        var token = Base64UrlEncoder.Encode(bytes);

        return token;
    }
}
