using Egeshka.Core.Domain.ValueObjects;

namespace Egeshka.Progress.Application.Model.Repository;

public sealed record ExerciseResultInsertModel(
    UserId UserId,
    SubjectId SubjectId,
    ExerciseId ExerciseId,
    IReadOnlyCollection<TaskId> ErrorTaskIds,
    int ExperiencePoints,
    DateTimeOffset Date);