namespace Egeshka.ApiGateway.Dtos.Progress.GetMyCompletedExercises;

/// <summary>
/// Запрос получения своих пройденных заданий
/// </summary>
public sealed class GetMyCompletedExercisesRequest
{
    /// <summary>
    /// Id предмета
    /// </summary>
    public long SubjectId { get; init; }
}
