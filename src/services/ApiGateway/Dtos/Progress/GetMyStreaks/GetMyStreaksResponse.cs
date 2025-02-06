namespace Egeshka.ApiGateway.Dtos.Progress.GetMyStreak;

/// <summary>
/// Ответ получения интервалов ударного режима
/// </summary>
public sealed class GetMyStreaksResponse
{
    /// <summary>
    /// Интервалы ударного режима
    /// </summary>
    public IReadOnlyCollection<Streak> Streaks { get; init; } = [];
}