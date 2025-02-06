using Egeshka.ApiGateway.Dtos.Progress.GetMyCompletedExercises;
using Egeshka.ApiGateway.Dtos.Progress.GetMyStreak;
using Microsoft.AspNetCore.Mvc;

namespace Egeshka.ApiGateway.Controllers;

[Route("/api/progress")]
public sealed class ProgressController : ControllerBaseWithIdentity
{
    [HttpGet("exercises/my")]
    [ProducesDefaultResponseType]
    [ProducesResponseType<GetMyCompletedExercisesResponse>(StatusCodes.Status200OK)]
    public Task<GetMyCompletedExercisesResponse> GetMyCompletedExercises(
        [FromQuery]GetMyCompletedExercisesRequest request,
        CancellationToken cancellationToken)
    {
        var response = new GetMyCompletedExercisesResponse()
        {
            ExerciseIds = [1, 2, 3]
        };

        return Task.FromResult(response);
    }

    [HttpGet("streak/my")]
    [ProducesDefaultResponseType]
    [ProducesResponseType<GetMyStreaksResponse>(StatusCodes.Status200OK)]
    public Task<GetMyStreaksResponse> GetMyStreak(CancellationToken cancellationToken)
    {
        var response = new GetMyStreaksResponse()
        {
            Streaks =
            [
                new()
                {
                    DateFrom = new DateOnly(2025, 1, 1),
                    DateTo = new DateOnly(2025, 1, 5),
                    FreezeDates =
                    [
                        new DateOnly(2025, 1, 2),
                        new DateOnly(2025, 1, 3)
                    ]
                },
                new()
                {
                    DateFrom = new DateOnly(2025, 1, 10),
                    DateTo = new DateOnly(2025, 1, 12),
                    FreezeDates =
                    [
                        new DateOnly(2025, 1, 12)
                    ]
                }
            ]
        };

        return Task.FromResult(response);
    }
}
