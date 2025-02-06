namespace Egeshka.ApiGateway.Dtos.Progress.GetMyStreak;

/// <summary>
/// Интервал ударного режима
/// </summary>
public sealed class Streak
{
    /// <summary>
    /// Начало интервала
    /// </summary>
    public DateTimeOffset DateFrom { get; init; }

    /// <summary>
    /// Конец интервала
    /// </summary>
    public DateTimeOffset DateTo { get; init; }

    /// <summary>
    /// Даты использования заморозки
    /// </summary>
    public IReadOnlyCollection<DateTimeOffset> FreezeDates { get; init; } = [];
}
