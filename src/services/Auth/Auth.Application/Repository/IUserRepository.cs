using Egeshka.Auth.Application.Models.Repository;
using Egeshka.Auth.Domain.Entities;

namespace Egeshka.Auth.Application.Repository;

public interface IUserRepository
{
    Task<UserCreationResult> CreateUserByRegistrationIdAsync(long registrationId, CancellationToken cancellationToken);
    Task InsertSessionAsync(SessionInsertModel session, CancellationToken cancellationToken);
    Task<Session?> GetSessionByRefreshToken(string refreshToken, CancellationToken cancellationToken);
    Task UpdateSessionAsync(SessionUpdateModel session, CancellationToken cancellationToken);
}
