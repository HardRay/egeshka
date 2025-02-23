using System.ComponentModel.DataAnnotations;

namespace Egeshka.ApiGateway.Dtos.Progress.SaveExerciseResult;

/// <summary>
/// Запрос сохранения результатов прохождения упражнения
/// </summary>
public class SaveExerciseResultRequest
{
    /// <summary>
    /// Id предмета
    /// </summary>
    [Required]
    public long SubjectId { get; init; }

    /// <summary>
    /// Id упражнения
    /// </summary>
    [Required]
    public long ExerciseId { get; init; }

    /// <summary>
    /// Id заданий, выполненных с ошибками
    /// </summary>
    public IReadOnlyCollection<long> ErrorTaskIds { get; init; } = [];

    /// <summary>
    /// Количество набранного опыта
    /// </summary>
    [Required]
    public int ExperiencePoints { get; init; }

    /// <summary>
    /// Дата выполнения упражнения
    /// </summary>
    [Required]
    public DateTime Date { get; init; }
}