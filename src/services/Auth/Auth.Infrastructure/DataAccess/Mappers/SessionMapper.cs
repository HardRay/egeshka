using Egeshka.Auth.Domain.Entities;
using Egeshka.Auth.Domain.ValueObjects;
using Egeshka.Auth.Infrastructure.DataAccess.DbModels;

namespace Egeshka.Auth.Infrastructure.DataAccess.Mappers;

public static class SessionMapper
{
    public static Session ToServiceModel(this SessionDbModel dbModel)
    {
        return new Session(
            Id: new SessionId(dbModel.Id),
            UserId: new UserId(dbModel.UserId),
            AccessToken: dbModel.AccessToken,
            RefreshToken: dbModel.RefreshToken,
            CreatedAt: dbModel.CreatedAt,
            UpdateAt: dbModel.UpdateAt);
    }
}
