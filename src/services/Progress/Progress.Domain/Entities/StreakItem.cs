using Egeshka.Core.Domain.Enums;
using Egeshka.Core.Domain.ValueObjects;

namespace Egeshka.Progress.Domain.Entities;

public sealed record StreakItem(
    UserId UserId,
    DateOnly Date,
    StreakItemType Type);
