using Dapper;
using Egeshka.Core.Application.Services.Interfaces;
using Egeshka.Core.Domain.ValueObjects;
using Egeshka.Progress.Application.Model.Repository;
using Egeshka.Progress.Infrastructure.DataAccess.Repositories.Internal.Interfaces;
using Npgsql;
using System.Text;

namespace Egeshka.Progress.Infrastructure.DataAccess.Repositories.Internal;

public sealed class ExerciseResultInternalRepository(
    IDateTimeProvider dateTimeProvider)
    : IExerciseResultInternalRepository
{
    private const string TableName = "exercise_results";

    public Task InsertAsync(
        NpgsqlConnection connection,
        ExerciseResultInsertModel exerciseResult,
        CancellationToken cancellationToken)
    {
        const string Sql =
            $"""
                insert into {TableName}(user_id, subject_id, exercise_id, error_task_ids, experience_points, done_date, create_at)
                values (@UserId, @SubjectId, @ExerciseId, @ErrorTaskIds, @ExperiencePoints, @Date, @CreateAt)
            """;

        var cmd = new CommandDefinition(
            Sql,
            new
            {
                UserId = exerciseResult.UserId.Value,
                SubjectId = exerciseResult.SubjectId.Value,
                ExerciseId = exerciseResult.ExerciseId.Value,
                ErrorTaskIds = exerciseResult.ErrorTaskIds.Select(id => id.Value).ToArray(),
                exerciseResult.ExperiencePoints,
                exerciseResult.Date,
                CreateAt = dateTimeProvider.UtcNow
            },
            cancellationToken: cancellationToken);

        return connection.ExecuteAsync(cmd);
    }

    public async Task<IReadOnlyCollection<ExerciseId>> GetCompletedExercises(
        NpgsqlConnection connection,
        UserId userId,
        SubjectId subjectId,
        CancellationToken cancellationToken)
    {
        const string Sql =
            $"""
                select exercise_id
                from {TableName}
                where user_id = @UserId and subject_id = @SubjectId
            """;

        var cmd = new CommandDefinition(
            Sql,
            new
            { 
                UserId = userId.Value,
                SubjectId = subjectId.Value
            },
            cancellationToken: cancellationToken);

        var exerciseIds = await connection.QueryAsync<long>(cmd);
        var result = exerciseIds.Select(id => new ExerciseId(id)).ToArray();

        return result;
    }
}
