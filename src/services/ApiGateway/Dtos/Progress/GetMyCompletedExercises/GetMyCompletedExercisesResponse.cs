namespace Egeshka.ApiGateway.Dtos.Progress.GetMyCompletedExercises;

/// <summary>
/// Ответ получения своих пройденных заданий
/// </summary>
public sealed class GetMyCompletedExercisesResponse
{
    /// <summary>
    /// Id пройденных упражнений
    /// </summary>
    public IReadOnlyCollection<long> ExerciseIds { get; init; } = [];
}
