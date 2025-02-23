using Egeshka.Auth.Application.Models;
using Egeshka.AuthBot.Models;
using Egeshka.Grpc.Auth;
using Telegram.Bot.Types;

namespace Egeshka.AuthBot.Mappers;

public static class AuthMapper
{
    public static RegistrationModel ToRegistrationModel(this Contact contact, long userId)
    {
        return new RegistrationModel(
            TelegramUserId: userId,
            MobileNumber: contact.PhoneNumber,
            FirstName: contact.FirstName,
            LastName: contact.LastName);
    }

    public static Registration.Types.Request ToProto(this RegistrationModel model)
    {
        return new Registration.Types.Request()
        {
            TelegramUserId = model.TelegramUserId,
            MobileNumber = model.MobileNumber,
            FirstName = model.FirstName,
            LastName = model.LastName
        };
    }

    public static RegistrationResult ToServiceModel(this Registration.Types.Response response)
    {
        return new RegistrationResult(response.RegistrationToken);
    }
}
