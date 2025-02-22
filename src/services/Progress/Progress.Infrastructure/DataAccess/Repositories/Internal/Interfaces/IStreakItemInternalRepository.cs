using Egeshka.Progress.Application.Model.Repository;
using Npgsql;

namespace Egeshka.Progress.Infrastructure.DataAccess.Repositories.Internal.Interfaces;

public interface IStreakItemInternalRepository
{
    Task InsertAsync(
        NpgsqlConnection connection,
        StreakItemInsertModel streakItem,
        CancellationToken cancellationToken);
}
