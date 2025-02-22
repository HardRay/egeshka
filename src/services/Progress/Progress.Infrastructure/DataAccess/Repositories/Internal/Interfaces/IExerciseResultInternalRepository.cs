using Egeshka.Core.Domain.ValueObjects;
using Egeshka.Progress.Application.Model.Repository;
using Npgsql;

namespace Egeshka.Progress.Infrastructure.DataAccess.Repositories.Internal.Interfaces;

public interface IExerciseResultInternalRepository
{
    Task InsertAsync(
        NpgsqlConnection connection,
        ExerciseResultInsertModel exerciseResult,
        CancellationToken cancellationToken);

    Task<IReadOnlyCollection<ExerciseId>> GetCompletedExercises(
        NpgsqlConnection connection,
        UserId userId,
        SubjectId subjectId,
        CancellationToken cancellationToken);
}
