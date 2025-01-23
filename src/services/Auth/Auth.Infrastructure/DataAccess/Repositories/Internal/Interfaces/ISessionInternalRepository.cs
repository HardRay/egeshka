using Egeshka.Auth.Application.Models.Repository;
using Egeshka.Auth.Infrastructure.DataAccess.DbModels;
using Npgsql;

namespace Egeshka.Auth.Infrastructure.DataAccess.Repositories.Internal.Interfaces;

public interface ISessionInternalRepository
{
    Task InsertAsync(NpgsqlConnection connection, SessionInsertModel session, CancellationToken cancellationToken);
    Task<SessionDbModel?> GetByRefreshTokenAsync(
        NpgsqlConnection connection, string refreshToken, CancellationToken cancellationToken, bool forUpdate = false);
    Task UpdateSession(NpgsqlConnection connection, SessionUpdateModel session, CancellationToken cancellationToken);
}
