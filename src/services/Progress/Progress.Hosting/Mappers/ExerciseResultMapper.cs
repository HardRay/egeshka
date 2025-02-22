using Egeshka.Core.Domain.ValueObjects;
using Egeshka.Progress.Application.Commands;
using Egeshka.Progress.Grpc;

namespace Egeshka.Progress.Hosting.Mappers;

public static class ExerciseResultMapper
{
    public static SaveExerciseResultCommand ToServiceCommand(this SaveExerciseResult.Types.Request request)
    {
        return new SaveExerciseResultCommand(
            UserId: new UserId(request.UserId),
            SubjectId: new SubjectId(request.SubjectId),
            ExerciseId: new ExerciseId(request.ExerciseId),
            ErrorTaskIds: request.ErrorTaskIds.Select(id => new TaskId(id)).ToArray(),
            ExperiencePoints: request.ExperiencePoints,
            Date: request.Date.ToDateTimeOffset());
    }
}
