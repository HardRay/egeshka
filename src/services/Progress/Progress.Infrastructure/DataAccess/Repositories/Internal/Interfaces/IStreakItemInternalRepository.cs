using Egeshka.Core.Domain.ValueObjects;
using Egeshka.Progress.Application.Model.Repository;
using Egeshka.Progress.Domain.Entities;
using Npgsql;

namespace Egeshka.Progress.Infrastructure.DataAccess.Repositories.Internal.Interfaces;

public interface IStreakItemInternalRepository
{
    Task InsertAsync(
        NpgsqlConnection connection,
        StreakItemInsertModel streakItem,
        CancellationToken cancellationToken);

    Task<IReadOnlyCollection<StreakItem>> GetUserStreakItemsAsync(
        NpgsqlConnection connection,
        UserId userId,
        CancellationToken cancellationToken);
}
