using Egeshka.Core.Domain.ValueObjects;

namespace Egeshka.Auth.Domain.Entities;

public sealed record User(
    UserId Id,
    TelegramUserId TelegramUserId,
    MobileNumber MobileNumber,
    string FirstName,
    string? LastName);
