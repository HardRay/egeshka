using Egeshka.Progress.Application.Model.Repository;

namespace Egeshka.Progress.Application.Repositories;

public interface IExerciseResultRepository
{
    Task InsertAsync(ExerciseResultInsertModel exerciseResult, CancellationToken cancellationToken);
}
