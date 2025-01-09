using Egeshka.Auth.Application.Commands.Registration;
using Egeshka.Auth.Application.Models;
using Egeshka.Auth.Domain.ValueObjects;
using Egeshka.Auth.Grpc;

namespace Egeshka.Auth.Mappers;

public static class RegistrationMapper
{
    public static RegistrationCommand ToServiceCommand(this Registration.Types.Request request)
    {
        return new RegistrationCommand(
            TelegramUserId: new TelegramUserId(request.TelegramUserId),
            MobileNumber: MobileNumber.Create(request.MobileNumber),
            FirstName: request.FirstName,
            LastName: request.LastName);
    }

    public static Registration.Types.Response ToProto(this RegistrationResult result)
    {
        return new Registration.Types.Response()
        {
            RegistrationToken = result.RegistrationToken
        };
    }
}
