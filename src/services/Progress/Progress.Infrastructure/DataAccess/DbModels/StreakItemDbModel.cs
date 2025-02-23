namespace Egeshka.Progress.Infrastructure.DataAccess.DbModels;

public sealed record StreakItemDbModel(
    long UserId,
    DateTime Date,
    int Type);
