using Egeshka.ApiGateway.Dtos.Progress.GetMyCompletedExercises;
using Egeshka.ApiGateway.Dtos.Progress.SaveExerciseResult;
using Egeshka.Core.Domain.ValueObjects;
using Egeshka.Grpc.Progress;
using Google.Protobuf.WellKnownTypes;

namespace Egeshka.ApiGateway.Mappers;

public static class ProgressMapper
{
    public static SaveExerciseResult.Types.Request ToProto(this SaveExerciseResultRequest request, UserId userId)
    {
        return new SaveExerciseResult.Types.Request()
        {
            UserId = userId.Value,
            SubjectId = request.SubjectId,
            ExerciseId = request.ExerciseId,
            ErrorTaskIds = { request.ErrorTaskIds },
            ExperiencePoints = request.ExperiencePoints,
            Date = request.Date.ToTimestamp()
        };
    }

    public static GetCompletedExercises.Types.Request ToProto(this GetMyCompletedExercisesRequest request, UserId userId)
    {
        return new GetCompletedExercises.Types.Request()
        {
            UserId = userId.Value,
            SubjectId = request.SubjectId
        };
    }

    public static GetMyCompletedExercisesResponse ToService(this GetCompletedExercises.Types.Response response)
    {
        return new GetMyCompletedExercisesResponse()
        {
            ExerciseIds = response.ExerciseIds
        };
    }
}
