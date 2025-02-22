using Egeshka.Core.Domain.ValueObjects;
using Egeshka.Progress.Application.Model.Repository;

namespace Egeshka.Progress.Application.Repositories;

public interface IExerciseResultRepository
{
    Task InsertAsync(ExerciseResultInsertModel exerciseResult, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<ExerciseId>> GetCompletedExercises(
        UserId userId,
        SubjectId subjectId,
        CancellationToken cancellationToken);
}
