using Egeshka.ApiGateway.Dtos.Progress.GetMyCompletedExercises;
using Egeshka.ApiGateway.Dtos.Progress.GetMyStreaks;
using Egeshka.ApiGateway.Dtos.Progress.SaveExerciseResult;
using Egeshka.Core.Domain.ValueObjects;

namespace Egeshka.ApiGateway.Providers.Interfaces;

public interface IProgressProvider
{
    Task SaveExerciseResultAsync(
        UserId userId,
        SaveExerciseResultRequest request,
        CancellationToken cancellationToken);

    Task<GetMyCompletedExercisesResponse> GetUserCompletedExercisesAsync(
        UserId userId,
        GetMyCompletedExercisesRequest request,
        CancellationToken cancellationToken);

    Task<GetMyStreaksResponse> GetUserStreaksAsync(
        UserId userId,
        CancellationToken cancellationToken);
}
