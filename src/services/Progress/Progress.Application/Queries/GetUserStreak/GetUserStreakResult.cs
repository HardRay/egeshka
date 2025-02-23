using Egeshka.Progress.Domain.Entities;

namespace Egeshka.Progress.Application.Queries.GetUserStreak;

public sealed record GetUserStreakResult(
    int CurrentStreak,
    IReadOnlyCollection<StreakItem> StreakItems);
