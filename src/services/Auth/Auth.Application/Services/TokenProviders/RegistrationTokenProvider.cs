using Egeshka.Auth.Application.Services.TokenProviders.Interfaces;

namespace Egeshka.Auth.Application.Services.TokenProviders;

public sealed class RegistrationTokenProvider : IRegistrationTokenProvider
{
    public string GenerateToken()
    {
        var time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
        var key = Guid.NewGuid().ToByteArray();
        var token = Convert.ToBase64String(time.Concat(key).ToArray());
        return token;
    }
}
