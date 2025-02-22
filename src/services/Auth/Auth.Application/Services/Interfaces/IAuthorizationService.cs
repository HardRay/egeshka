using Egeshka.Auth.Application.Models;
using Egeshka.Core.Domain.ValueObjects;

namespace Egeshka.Auth.Application.Services.Interfaces;

public interface IAuthorizationService
{
    Task<AuthorizationData> GenerateAuthorizationDataAsync(UserId userId, CancellationToken cancellationToken);
}
