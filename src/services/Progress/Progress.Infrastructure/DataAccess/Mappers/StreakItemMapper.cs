using Egeshka.Core.Domain.Enums;
using Egeshka.Core.Domain.ValueObjects;
using Egeshka.Progress.Domain.Entities;
using Egeshka.Progress.Infrastructure.DataAccess.DbModels;

namespace Egeshka.Progress.Infrastructure.DataAccess.Mappers;

public static class StreakItemMapper
{
    public static StreakItem ToServiceModel(this StreakItemDbModel model)
    {
        return new StreakItem(
            UserId: new UserId(model.UserId),
            Date: DateOnly.FromDateTime(model.Date),
            Type: (StreakItemType)model.Type);
    }
}
