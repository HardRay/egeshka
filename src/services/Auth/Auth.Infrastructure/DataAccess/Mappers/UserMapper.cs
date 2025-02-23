using Egeshka.Auth.Domain.Entities;
using Egeshka.Auth.Infrastructure.DataAccess.DbModels;
using Egeshka.Core.Domain.ValueObjects;

namespace Egeshka.Auth.Infrastructure.DataAccess.Mappers;

public static class UserMapper
{
    public static User ToServiceModel(this UserDbModel model)
    {
        return new User(
            Id: new UserId(model.Id),
            TelegramUserId: new TelegramUserId(model.TelegramUserId),
            MobileNumber: MobileNumber.Create(model.MobileNumber),
            FirstName: model.FirstName,
            LastName: model.LastName);
    }
}
