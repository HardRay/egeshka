using Egeshka.Core.Domain.Enums;
using Egeshka.Core.Domain.ValueObjects;

namespace Egeshka.Progress.Application.Model.Repository;

public sealed record StreakItemInsertModel(
    UserId UserId,
    DateOnly Date,
    StreakItemType Type);
