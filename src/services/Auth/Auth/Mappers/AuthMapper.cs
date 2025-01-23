using Egeshka.Auth.Application.Commands.Login;
using Egeshka.Auth.Application.Commands.Registration;
using Egeshka.Auth.Application.Commands.Relogin;
using Egeshka.Auth.Domain.ValueObjects;
using Egeshka.Auth.Grpc;

namespace Egeshka.Auth.Mappers;

public static class AuthMapper
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

    public static LoginCommand ToServiceCommand(this Login.Types.Request request)
    {
        return new LoginCommand(RegistrationToken: request.RegistrationToken);
    }

    public static Login.Types.Response ToProto(this LoginResult result)
    {
        return new Login.Types.Response()
        {
            AccessToken = result.Data.AccessToken,
            RefreshToken = result.Data.RefreshToken
        };
    }

    public static ReloginCommand ToServiceCommand(this Relogin.Types.Request request)
    {
        return new ReloginCommand(RefreshToken: request.RefreshToken);
    }

    public static Relogin.Types.Response ToProto(this ReloginResult result)
    {
        return new Relogin.Types.Response()
        {
            AccessToken = result.Data.AccessToken,
            RefreshToken = result.Data.RefreshToken
        };
    }
}
