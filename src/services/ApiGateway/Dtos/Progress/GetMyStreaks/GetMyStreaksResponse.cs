using System.ComponentModel.DataAnnotations;

namespace Egeshka.ApiGateway.Dtos.Progress.GetMyStreaks;

/// <summary>
/// Ответ получения интервалов ударного режима
/// </summary>
public sealed class GetMyStreaksResponse
{
    /// <summary>
    /// Количество дней в текущем ударном режиме
    /// </summary>
    [Required]
    public int CurrentStreak { get; init; }

    /// <summary>
    /// Интервалы ударного режима
    /// </summary>
    public IReadOnlyCollection<StreakItem> Streaks { get; init; } = [];
}