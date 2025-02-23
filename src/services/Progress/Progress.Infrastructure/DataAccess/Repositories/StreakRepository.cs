using Egeshka.Core.Domain.ValueObjects;
using Egeshka.Progress.Application.Repositories;
using Egeshka.Progress.Domain.Entities;
using Egeshka.Progress.Infrastructure.DataAccess.Repositories.Common;
using Egeshka.Progress.Infrastructure.DataAccess.Repositories.Internal.Interfaces;

namespace Egeshka.Progress.Infrastructure.DataAccess.Repositories;

public sealed class StreakRepository(
    IPostgresConnectionFactory connectionFactory,
    IStreakItemInternalRepository internalRepository)
    : IStreakRepository
{
    public async Task<IReadOnlyCollection<StreakItem>> GetUserStreakAsync(UserId userId, CancellationToken cancellationToken)
    {
        using var connection = connectionFactory.GetConnection();
        await connection.OpenAsync(cancellationToken);

        var result = await internalRepository.GetUserStreakItemsAsync(connection, userId, cancellationToken);
        return result;
    }
}
