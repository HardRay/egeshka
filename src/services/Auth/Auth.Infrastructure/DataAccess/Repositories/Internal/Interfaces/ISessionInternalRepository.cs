using Egeshka.Auth.Application.Models.Repository;
using Npgsql;

namespace Egeshka.Auth.Infrastructure.DataAccess.Repositories.Internal.Interfaces;

public interface ISessionInternalRepository
{
    Task InsertAsync(NpgsqlConnection connection, SessionInsertModel session, CancellationToken cancellationToken);
}
