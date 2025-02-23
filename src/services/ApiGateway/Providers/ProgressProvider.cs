using Egeshka.ApiGateway.Dtos.Progress.GetMyCompletedExercises;
using Egeshka.ApiGateway.Dtos.Progress.GetMyStreaks;
using Egeshka.ApiGateway.Dtos.Progress.SaveExerciseResult;
using Egeshka.ApiGateway.Mappers;
using Egeshka.ApiGateway.Providers.Interfaces;
using Egeshka.Core.Domain.ValueObjects;
using Egeshka.Grpc.Progress;

namespace Egeshka.ApiGateway.Providers;

public sealed class ProgressProvider(
    ProgressGrpc.ProgressGrpcClient progressClient,
    StreakGrpc.StreakGrpcClient streakClient)
    : IProgressProvider
{
    public async Task SaveExerciseResultAsync(
        UserId userId,
        SaveExerciseResultRequest request,
        CancellationToken cancellationToken)
    {
        await progressClient.SaveExerciseResultAsync(request.ToProto(userId), cancellationToken: cancellationToken);
    }

    public async Task<GetMyCompletedExercisesResponse> GetUserCompletedExercisesAsync(
        UserId userId,
        GetMyCompletedExercisesRequest request,
        CancellationToken cancellationToken)
    {
        var response = await progressClient.GetCompletedExercisesAsync(request.ToProto(userId), cancellationToken: cancellationToken);

        return response.ToService();
    }

    public async Task<GetMyStreaksResponse> GetUserStreaksAsync(
        UserId userId,
        CancellationToken cancellationToken)
    {
        var request = new GetUserStreak.Types.Request() { UserId = userId.Value };
        var response = await streakClient.GetUserStreakAsync(request, cancellationToken: cancellationToken);

        return response.ToService();
    }
}
