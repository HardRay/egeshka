using Egeshka.Auth.Domain.ValueObjects;

namespace Egeshka.Auth.Application.Services.TokenGenerators.Interfaces;

public interface IRefreshTokenGenerator
{
    string GenerateToken(UserId userId);
}
