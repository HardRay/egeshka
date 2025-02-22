using Dapper;
using Egeshka.Core.Application.Services.Interfaces;
using Egeshka.Progress.Application.Model.Repository;
using Egeshka.Progress.Infrastructure.DataAccess.Repositories.Internal.Interfaces;
using Npgsql;

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
}
