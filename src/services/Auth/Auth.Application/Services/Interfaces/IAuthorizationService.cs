using Egeshka.Auth.Application.Models;

namespace Egeshka.Auth.Application.Services.Interfaces;

public interface IAuthorizationService
{
    Task<AuthorizationData> GenerateAuthorizationDataAsync(long userId, CancellationToken cancellationToken);
}
