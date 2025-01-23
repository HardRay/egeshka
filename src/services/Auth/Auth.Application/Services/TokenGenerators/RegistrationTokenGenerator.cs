using Egeshka.Auth.Application.Services.TokenGenerators.Interfaces;

namespace Egeshka.Auth.Application.Services.TokenGenerators;

public sealed class RegistrationTokenGenerator : IRegistrationTokenGenerator
{
    public string GenerateToken()
    {
        var time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
        var key = Guid.NewGuid().ToByteArray();
        var token = Convert.ToBase64String(time.Concat(key).ToArray());
        return token;
    }
}
