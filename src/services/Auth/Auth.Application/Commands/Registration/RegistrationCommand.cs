using Egeshka.Auth.Domain.ValueObjects;
using MediatR;

namespace Egeshka.Auth.Application.Commands.Registration;

public sealed record RegistrationCommand(
    TelegramUserId TelegramUserId,
    MobileNumber MobileNumber,
    string FirstName,
    string? LastName)
    : IRequest<RegistrationResult>;
