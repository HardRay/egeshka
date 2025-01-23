namespace Egeshka.Auth.Infrastructure.DataAccess.DbModels;

public sealed record RegistrationDbModel(
    long Id,
    long TelegramUserId,
    string MobileNumber,
    string FirstName,
    string? LastName,
    string RegistrationToken,
    DateTime CreatedAt,
    DateTime UpdateAt);