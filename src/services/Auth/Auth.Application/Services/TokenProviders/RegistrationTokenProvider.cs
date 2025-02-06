using Egeshka.Auth.Application.Services.TokenProviders.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Egeshka.Auth.Application.Services.TokenProviders;

public sealed class RegistrationTokenProvider : IRegistrationTokenProvider
{
    public string GenerateToken()
    {
        var time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
        var key = Guid.NewGuid().ToByteArray();
        var token = Base64UrlEncoder.Encode(time.Concat(key).ToArray());
        return token;
    }
}
