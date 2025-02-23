using Egeshka.Core.Domain.Enums;

namespace Egeshka.Progress.Infrastructure.DataAccess.DbModels;

public sealed record StreakItemDbModel(
    long UserId,
    DateOnly Date,
    StreakItemType Type);
