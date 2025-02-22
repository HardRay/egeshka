using Egeshka.Core.Domain.ValueObjects;

namespace Egeshka.Progress.Application.Queries.GetCompletedExercises;

public sealed record GetCompletedExercisesResult(IReadOnlyCollection<ExerciseId> ExerciseIds);
