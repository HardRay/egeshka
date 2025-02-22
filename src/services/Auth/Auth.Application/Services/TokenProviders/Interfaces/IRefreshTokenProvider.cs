using Egeshka.Core.Domain.ValueObjects;

namespace Egeshka.Auth.Application.Services.TokenProviders.Interfaces;

public interface IRefreshTokenProvider
{
    string GenerateToken(UserId userId);
}
