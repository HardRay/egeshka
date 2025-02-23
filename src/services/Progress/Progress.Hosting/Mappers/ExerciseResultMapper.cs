using Egeshka.Core.Domain.ValueObjects;
using Egeshka.Progress.Application.Commands.SaveExerciseResult;
using Egeshka.Progress.Application.Queries.GetCompletedExercises;
using Egeshka.Grpc.Progress;

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

    public static GetCompletedExercisesQuery ToServiceQuery(this GetCompletedExercises.Types.Request request)
    {
        return new GetCompletedExercisesQuery(
            UserId: new UserId(request.UserId),
            SubjectId: new SubjectId(request.SubjectId));
    }

    public static GetCompletedExercises.Types.Response ToProto(this GetCompletedExercisesResult result)
    {
        return new GetCompletedExercises.Types.Response()
        {
            ExerciseIds = { result.ExerciseIds.Select(id => id.Value) }
        };
    }
}
