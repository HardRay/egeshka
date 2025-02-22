using Egeshka.Core.Domain.ValueObjects;
using Egeshka.Progress.Application.Model.Repository;
using Egeshka.Progress.Application.Repositories;
using Egeshka.Progress.Infrastructure.DataAccess.Mappers;
using Egeshka.Progress.Infrastructure.DataAccess.Repositories.Common;
using Egeshka.Progress.Infrastructure.DataAccess.Repositories.Internal.Interfaces;

namespace Egeshka.Progress.Infrastructure.DataAccess.Repositories;

public sealed class ExerciseResultRepository(
    IPostgresConnectionFactory connectionFactory,
    IExerciseResultInternalRepository exerciseResultInternalRepository,
    IStreakItemInternalRepository streakItemInternalRepository)
    : IExerciseResultRepository
{
    public async Task InsertAsync(ExerciseResultInsertModel exerciseResult, CancellationToken cancellationToken)
    {
        using var connection = connectionFactory.GetConnection();
        await connection.OpenAsync(cancellationToken);

        using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        await exerciseResultInternalRepository.InsertAsync(connection, exerciseResult, cancellationToken);
        await streakItemInternalRepository.InsertAsync(connection, exerciseResult.ToStreakItem(), cancellationToken);

        await transaction.CommitAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<ExerciseId>> GetCompletedExercises(
        UserId userId,
        SubjectId subjectId,
        CancellationToken cancellationToken)
    {
        using var connection = connectionFactory.GetConnection();
        await connection.OpenAsync(cancellationToken);

        var result = await exerciseResultInternalRepository.GetCompletedExercises(connection, userId, subjectId, cancellationToken);
        return result;
    }
}
