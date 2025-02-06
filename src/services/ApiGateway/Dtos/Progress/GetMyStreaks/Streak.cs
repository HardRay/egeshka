namespace Egeshka.ApiGateway.Dtos.Progress.GetMyStreak;

/// <summary>
/// Интервал ударного режима
/// </summary>
public sealed class Streak
{
    /// <summary>
    /// Начало интервала
    /// </summary>
    public DateOnly DateFrom { get; init; }

    /// <summary>
    /// Конец интервала
    /// </summary>
    public DateOnly DateTo { get; init; }

    /// <summary>
    /// Даты использования заморозки
    /// </summary>
    public IReadOnlyCollection<DateOnly> FreezeDates { get; init; } = [];
}
