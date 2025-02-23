namespace Egeshka.Auth.Infrastructure.DataAccess.DbModels;

public sealed record SessionDbModel(
    long Id,
    long UserId,
    string RefreshToken,
    DateTime CreatedAt,
    DateTime UpdateAt);