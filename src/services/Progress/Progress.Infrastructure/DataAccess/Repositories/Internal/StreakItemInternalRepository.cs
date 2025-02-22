using Dapper;
using Egeshka.Progress.Application.Model.Repository;
using Egeshka.Progress.Infrastructure.DataAccess.Repositories.Internal.Interfaces;
using Npgsql;

namespace Egeshka.Progress.Infrastructure.DataAccess.Repositories.Internal;

public sealed class StreakItemInternalRepository : IStreakItemInternalRepository
{
    private const string TableName = "streak_items";

    public Task InsertAsync(
        NpgsqlConnection connection,
        StreakItemInsertModel streakItem,
        CancellationToken cancellationToken)
    {
        const string Sql =
            $"""
                insert into {TableName}(user_id, date, type)
                values (@UserId, @Date, @Type)
                on conflict do nothing;
            """;

        var cmd = new CommandDefinition(
            Sql,
            new
            {
                UserId = streakItem.UserId.Value,
                streakItem.Date,
                Type = streakItem.Type
            },
            cancellationToken: cancellationToken);

        return connection.ExecuteAsync(cmd);
    }
}
