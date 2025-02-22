using Dapper;
using Egeshka.Progress.Application.Model.Repository;
using Egeshka.Progress.Infrastructure.DataAccess.Repositories.Internal.Interfaces;
using Npgsql;

namespace Egeshka.Progress.Infrastructure.DataAccess.Repositories.Internal;

public sealed class ExerciseResultInternalRepository : IExerciseResultInternalRepository
{
    private const string TableName = "exercise_results";

    public Task InsertAsync(
        NpgsqlConnection connection,
        ExerciseResultInsertModel exerciseResult,
        CancellationToken cancellationToken)
    {
        const string Sql =
            $"""
                insert into {TableName}(user_id, subject_id, exercise_id, error_task_ids, experience_points, create_at)
                values (@UserId, @SubjectId, @ExerciseId, @ErrorTaskIds, @ExperiencePoints, @Date)
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
            },
            cancellationToken: cancellationToken);

        return connection.ExecuteAsync(cmd);
    }
}
