using Egeshka.Auth.Infrastructure.DataAccess.DbModels;
using Npgsql;

namespace Egeshka.Auth.Infrastructure.DataAccess.Repositories.Internal.Interfaces;

public interface IRegistrationInternalRepository
{
    Task<RegistrationDbModel?> GetByIdAsync(
        NpgsqlConnection connection, long id, CancellationToken cancellationToken, bool forUpdate = false);
    Task DeleteByIdAsync(NpgsqlConnection connection, long id, CancellationToken cancellationToken);
}
