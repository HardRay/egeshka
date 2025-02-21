using Egeshka.Core.Domain.ValueObjects;

namespace Egeshka.Progress.Domain.Entities;

public sealed record ExerciseResult(
    ExerciseResultId Id,
    UserId UserId,
    SubjectId SubjectId,
    ExerciseId ExerciseId,
    IReadOnlyCollection<TaskId> ErrorTaskIds,
    int ExperiencePoints);
