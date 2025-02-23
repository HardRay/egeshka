using Egeshka.Progress.Application.Commands.SaveExerciseResult;
using Egeshka.Progress.Application.Model.Repository;

namespace Egeshka.Progress.Application.Mappers;

public static class ExerciseResultMapper
{
    public static ExerciseResultInsertModel ToInsertModel(this SaveExerciseResultCommand command)
    {
        return new ExerciseResultInsertModel(
            UserId: command.UserId,
            SubjectId: command.SubjectId,
            ExerciseId: command.ExerciseId,
            ErrorTaskIds: command.ErrorTaskIds
                .DistinctBy(id => id.Value)
                .OrderBy(id => id.Value)
                .ToArray(),
            ExperiencePoints: command.ExperiencePoints,
            Date: command.Date);
    }
}
