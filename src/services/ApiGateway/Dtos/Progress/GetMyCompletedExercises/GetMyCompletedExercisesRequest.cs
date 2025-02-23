using System.ComponentModel.DataAnnotations;

namespace Egeshka.ApiGateway.Dtos.Progress.GetMyCompletedExercises;

/// <summary>
/// Запрос получения своих пройденных упражнений
/// </summary>
public sealed class GetMyCompletedExercisesRequest
{
    /// <summary>
    /// Id предмета
    /// </summary>
    [Required]
    public long SubjectId { get; init; }
}
