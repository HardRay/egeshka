using Egeshka.Core.Domain.ValueObjects;
using Egeshka.Progress.Domain.Entities;

namespace Egeshka.Progress.Application.Repositories;

public interface IStreakRepository
{
    Task<IReadOnlyCollection<StreakItem>> GetUserStreakAsync(UserId userId, CancellationToken cancellationToken);
}
