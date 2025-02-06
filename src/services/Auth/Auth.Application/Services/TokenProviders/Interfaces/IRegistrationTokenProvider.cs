namespace Egeshka.Auth.Application.Services.TokenProviders.Interfaces;

public interface IRegistrationTokenProvider
{
    string GenerateToken();
}
