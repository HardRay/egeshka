using Egeshka.Auth.Application.Commands.Registration;
using Egeshka.Auth.Application.Models.Repository;

namespace Egeshka.Auth.Application.Mappers;

public static class RegistrationMapper
{
    public static RegistrationInsertModel ToInsertModel(this RegistrationCommand command, string registrationToken)
    {
        return new RegistrationInsertModel(
            TelegramUserId: command.TelegramUserId,
            MobileNumber: command.MobileNumber,
            FirstName: command.FirstName,
            LastName: command.LastName,
            RegistrationToken: registrationToken);
    }
}
