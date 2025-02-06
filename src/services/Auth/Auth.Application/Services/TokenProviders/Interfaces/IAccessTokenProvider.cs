using Egeshka.Auth.Domain.ValueObjects;

namespace Egeshka.Auth.Application.Services.TokenProviders.Interfaces;

public interface IAccessTokenProvider
{
    string GenerateToken(UserId userId);
}
