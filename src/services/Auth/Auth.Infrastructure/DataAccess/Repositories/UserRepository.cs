using Egeshka.Auth.Application.Models.Repository;
using Egeshka.Auth.Application.Repository;
using Egeshka.Auth.Domain.Entities;
using Egeshka.Auth.Infrastructure.DataAccess.Mappers;
using Egeshka.Auth.Infrastructure.DataAccess.Repositories.Common;
using Egeshka.Auth.Infrastructure.DataAccess.Repositories.Internal.Interfaces;
using Egeshka.Core.Domain.ValueObjects;
using Egeshka.Core.Models.Exceptions;

namespace Egeshka.Auth.Infrastructure.DataAccess.Repositories;

public sealed class UserRepository(
    IPostgresConnectionFactory connectionFactory,
    IRegistrationInternalRepository registrationRepository,
    IUserInternalRepository userInternalRepository,
    ISessionInternalRepository sessionRepository) : IUserRepository
{
    public async Task<UserCreationResult> CreateUserByRegistrationIdAsync(long registrationId, CancellationToken cancellationToken)
    {
        using var connection = connectionFactory.GetConnection();
        await connection.OpenAsync(cancellationToken);

        using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        var registration = await registrationRepository.GetByIdAsync(connection, registrationId, cancellationToken)
            ?? throw new EntityNotFoundException($"Не найдена регистрация c Id {registrationId}");

        var userId = await userInternalRepository.InsertUserAsync(connection, registration.ToUserInsertModel(), cancellationToken);
        await registrationRepository.DeleteByIdAsync(connection, registrationId, cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return new UserCreationResult(new UserId(userId));
    }

    public async Task InsertSessionAsync(SessionInsertModel session, CancellationToken cancellationToken)
    {
        using var connection = connectionFactory.GetConnection();
        await connection.OpenAsync(cancellationToken);

        await sessionRepository.InsertAsync(connection, session, cancellationToken);
    }

    public async Task<Session?> GetSessionByRefreshToken(string refreshToken, CancellationToken cancellationToken)
    {
        using var connection = connectionFactory.GetConnection();
        await connection.OpenAsync(cancellationToken);

        var session = await sessionRepository.GetByRefreshTokenAsync(connection, refreshToken, cancellationToken);

        return session?.ToServiceModel();
    }

    public async Task UpdateSessionAsync(SessionUpdateModel session, CancellationToken cancellationToken)
    {
        using var connection = connectionFactory.GetConnection();
        await connection.OpenAsync(cancellationToken);

        await sessionRepository.UpdateSession(connection, session, cancellationToken);
    }
}
