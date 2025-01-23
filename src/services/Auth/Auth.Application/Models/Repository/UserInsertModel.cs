using Egeshka.Auth.Domain.ValueObjects;

namespace Egeshka.Auth.Application.Models.Repository;

public sealed record UserInsertModel(
    TelegramUserId TelegramUserId,
    MobileNumber MobileNumber,
    string FirstName,
    string? LastName);