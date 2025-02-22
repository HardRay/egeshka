using Egeshka.Progress.Application.Commands;
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
            ErrorTaskIds: command.ErrorTaskIds,
            ExperiencePoints: command.ExperiencePoints,
            Date: command.Date);
    }
}
