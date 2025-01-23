using Egeshka.Auth.Application.Services.TokenGenerators.Interfaces;
using Egeshka.Auth.Domain.ValueObjects;

namespace Egeshka.Auth.Application.Services.TokenGenerators;

public sealed class RefreshTokenGenerator : IRefreshTokenGenerator
{
    public string GenerateToken(UserId userId)
    {
        var user = BitConverter.GetBytes(userId);
        var time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
        var key = Guid.NewGuid().ToByteArray();

        var bytes = user.Concat(time).Concat(key).ToArray();
        var token = Convert.ToBase64String(bytes);

        return token;
    }
}
