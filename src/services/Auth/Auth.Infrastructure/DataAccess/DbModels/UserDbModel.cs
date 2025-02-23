namespace Egeshka.Auth.Infrastructure.DataAccess.DbModels;

public sealed record UserDbModel(
    long Id,
    long TelegramUserId,
    string MobileNumber,
    string FirstName,
    string? LastName);
