using Egeshka.Auth.Application.Models.Repository;
using Egeshka.Auth.Domain.Entities;
using Egeshka.Auth.Domain.ValueObjects;
using Egeshka.Auth.Infrastructure.DataAccess.DbModels;

namespace Egeshka.Auth.Infrastructure.DataAccess.Mappers;

public static class RegistrationMapper
{
    public static Registration ToServiceModel(this RegistrationDbModel dbModel)
    {
        return new Registration(
            Id: new RegistrationId(dbModel.Id),
            TelegramUserId: new TelegramUserId(dbModel.TelegramUserId),
            MobileNumber: MobileNumber.Create(dbModel.MobileNumber),
            FirstName: dbModel.FirstName,
            LastName: dbModel.LastName,
            RegistrationToken: dbModel.RegistrationToken,
            CreatedAt: dbModel.CreatedAt,
            UpdateAt: dbModel.UpdateAt);
    }

    public static UserInsertModel ToUserInsertModel(this RegistrationDbModel model)
    {
        return new UserInsertModel(
            TelegramUserId: new(model.TelegramUserId),
            MobileNumber: MobileNumber.Create(model.MobileNumber),
            FirstName: model.FirstName,
            LastName: model.LastName);
    }
}
