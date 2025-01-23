using Egeshka.Auth.Application.Models.Repository;
using Npgsql;

namespace Egeshka.Auth.Infrastructure.DataAccess.Repositories.Internal.Interfaces;

public interface IUserInternalRepository
{
    Task<long> InsertUserAsync(NpgsqlConnection connection, UserInsertModel user, CancellationToken cancellationToken);
}
