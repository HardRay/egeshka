using Egeshka.Auth.Application.Models;
using Egeshka.Auth.Domain.ValueObjects;
using Egeshka.Auth.Infrastructure.DataAccess.DbModels;

namespace Egeshka.Auth.Infrastructure.DataAccess.Mappers;

public static class RegistrationMapper
{
    public static RegistrationModel ToServiceModel(this RegistrationDbModel dbModel)
    {
        return new RegistrationModel(
            Id: dbModel.Id,
            TelegramUserId: new TelegramUserId(dbModel.TelegramUserId),
            MobileNumber: MobileNumber.Create(dbModel.MobileNumber),
            FirstName: dbModel.FirstName,
            LastName: dbModel.LastName,
            RegistrationToken: dbModel.RegistrationToken,
            CreatedAt: dbModel.CreatedAt);
    }
}
