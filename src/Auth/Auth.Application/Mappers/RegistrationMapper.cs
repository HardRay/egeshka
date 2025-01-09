using Egeshka.Auth.Application.Commands.Registration;
using Egeshka.Auth.Application.Models;

namespace Egeshka.Auth.Application.Mappers;

public static class RegistrationMapper
{
    public static RegistrationModel ToModel(this RegistrationCommand command, string registrationToken)
    {
        return new RegistrationModel(
            TelegramUserId: command.TelegramUserId,
            MobileNumber: command.MobileNumber,
            FirstName: command.FirstName,
            LastName: command.LastName,
            RegistrationToken: registrationToken);
    }
}
