using Egeshka.Auth.Domain.ValueObjects;

namespace Egeshka.Auth.Application.Models;

public sealed record RegistrationModel(
    TelegramUserId TelegramUserId,
    MobileNumber MobileNumber,
    string FirstName,
    string? LastName,
    string RegistrationToken);
