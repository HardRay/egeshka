using Egeshka.Core.Domain.Enums;

namespace Egeshka.ApiGateway.Dtos.Progress.GetMyStreaks;

/// <summary>
/// Элемент ударного режима
/// </summary>
public sealed class StreakItem
{
    /// <summary>
    /// Дата продления ударного режима
    /// </summary>
    public DateOnly Date { get; init; }

    /// <summary>
    /// Способ продления ударного режима
    /// </summary>
    public StreakItemType Type { get; init; }
}
