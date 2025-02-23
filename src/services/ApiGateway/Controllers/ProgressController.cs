using Egeshka.ApiGateway.Dtos.Progress.GetMyCompletedExercises;
using Egeshka.ApiGateway.Dtos.Progress.GetMyStreaks;
using Egeshka.ApiGateway.Dtos.Progress.SaveExerciseResult;
using Egeshka.ApiGateway.Providers.Interfaces;
using Egeshka.Core.Models.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Egeshka.ApiGateway.Controllers;

[Route("/api/progress")]
public sealed class ProgressController(
    IProgressProvider provider)
    : ControllerBaseWithIdentity
{
    /// <summary>
    /// Сохранение результата упражнения
    /// </summary>
    [HttpPost("save")]
    [ProducesDefaultResponseType]
    [ProducesResponseType<GetMyCompletedExercisesResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorModel>(StatusCodes.Status400BadRequest)]
    public Task SaveExerciseResult(
        [FromBody] SaveExerciseResultRequest request,
        CancellationToken cancellationToken)
    {
        var userId = GetUserIdOrThrow();

        return provider.SaveExerciseResultAsync(userId, request, cancellationToken);
    }

    /// <summary>
    /// Получение своих пройденных упрежнений
    /// </summary>
    [HttpGet("exercises/my")]
    [ProducesDefaultResponseType]
    [ProducesResponseType<GetMyCompletedExercisesResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorModel>(StatusCodes.Status400BadRequest)]
    public Task<GetMyCompletedExercisesResponse> GetMyCompletedExercises(
        [FromQuery] GetMyCompletedExercisesRequest request,
        CancellationToken cancellationToken)
    {
        var userId = GetUserIdOrThrow();

        return provider.GetUserCompletedExercisesAsync(userId, request, cancellationToken);
    }

    /// <summary>
    /// Получение своих ударных режимов
    /// </summary>
    [HttpGet("streak/my")]
    [ProducesDefaultResponseType]
    [ProducesResponseType<GetMyStreaksResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorModel>(StatusCodes.Status400BadRequest)]
    public Task<GetMyStreaksResponse> GetMyStreak(CancellationToken cancellationToken)
    {
        var userId = GetUserIdOrThrow();

        return provider.GetUserStreaksAsync(userId, cancellationToken);
    }
}
