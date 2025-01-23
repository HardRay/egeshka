namespace Egeshka.Auth.Application.Services.TokenGenerators.Interfaces;

public interface IAccessTokenGenerator
{
    string GenerateToken(long userId);
}
