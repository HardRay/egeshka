using Egeshka.Auth.Domain.ValueObjects;

namespace Egeshka.Auth.Application.Services.TokenGenerators.Interfaces;

public interface IAccessTokenGenerator
{
    string GenerateToken(UserId userId);
}
