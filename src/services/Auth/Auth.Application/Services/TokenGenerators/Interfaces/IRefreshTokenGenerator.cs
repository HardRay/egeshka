namespace Egeshka.Auth.Application.Services.TokenGenerators.Interfaces;

public interface IRefreshTokenGenerator
{
    string GenerateToken(long userId);
}
