using Egeshka.Auth.Application.Models.Repository;

namespace Egeshka.Auth.Application.Repository;

public interface IUserRepository
{
    Task<UserCreationResult> CreateUserByRegistrationIdAsync(long registrationId, CancellationToken cancellationToken);
    Task InsertSessionAsync(SessionInsertModel session, CancellationToken cancellationToken);
}
