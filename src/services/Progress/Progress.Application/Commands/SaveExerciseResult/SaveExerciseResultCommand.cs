using Egeshka.Core.Domain.ValueObjects;
using MediatR;

namespace Egeshka.Progress.Application.Commands.SaveExerciseResult;

public sealed record SaveExerciseResultCommand(
    UserId UserId,
    SubjectId SubjectId,
    ExerciseId ExerciseId,
    IReadOnlyCollection<TaskId> ErrorTaskIds,
    int ExperiencePoints,
    DateTimeOffset Date)
    : IRequest;