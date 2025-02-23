using Egeshka.Progress.Domain.Entities;
using Egeshka.Progress.Infrastructure.DataAccess.DbModels;
using Egeshka.Core.Domain.ValueObjects;

namespace Egeshka.Progress.Infrastructure.DataAccess.Mappers;

public static class StreakItemMapper
{
    public static StreakItem ToServiceModel(this StreakItemDbModel model)
    {
        return new StreakItem(
            UserId: new UserId(model.UserId),
            Date: model.Date,
            Type: model.Type);
    }
}
