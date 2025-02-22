using Egeshka.Core.Domain.Enums;
using Egeshka.Progress.Application.Model.Repository;

namespace Egeshka.Progress.Infrastructure.DataAccess.Mappers;

public static class ExerciseResultMapper
{
    public static StreakItemInsertModel ToStreakItem(this ExerciseResultInsertModel exerciseResult)
    {
        return new StreakItemInsertModel(
            UserId: exerciseResult.UserId,
            Date: DateOnly.FromDateTime(exerciseResult.Date.Date),
            Type: StreakItemType.Progress);
    }
}
