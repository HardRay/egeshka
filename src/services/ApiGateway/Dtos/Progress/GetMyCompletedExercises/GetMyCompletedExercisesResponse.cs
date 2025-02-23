using System.ComponentModel.DataAnnotations;

namespace Egeshka.ApiGateway.Dtos.Progress.GetMyCompletedExercises;

/// <summary>
/// Ответ получения своих пройденных упражнений
/// </summary>
public sealed class GetMyCompletedExercisesResponse
{
    /// <summary>
    /// Id пройденных упражнений
    /// </summary>
    [Required]
    public IReadOnlyCollection<long> ExerciseIds { get; init; } = [];
}
