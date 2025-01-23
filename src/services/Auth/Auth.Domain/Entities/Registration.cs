using Egeshka.Auth.Domain.ValueObjects;

namespace Egeshka.Auth.Domain.Entities;

public sealed record Registration(
    RegistrationId Id,
    TelegramUserId TelegramUserId,
    MobileNumber MobileNumber,
    string FirstName,
    string? LastName,
    string RegistrationToken,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdateAt);
