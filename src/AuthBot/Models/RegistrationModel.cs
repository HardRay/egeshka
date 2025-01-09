namespace Egeshka.AuthBot.Models;

public sealed record RegistrationModel(
    long TelegramUserId,
    string MobileNumber,
    string FirstName,
    string? LastName);
