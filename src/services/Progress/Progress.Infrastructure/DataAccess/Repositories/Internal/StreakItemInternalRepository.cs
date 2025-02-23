using Dapper;
using Egeshka.Core.Domain.ValueObjects;
using Egeshka.Progress.Application.Model.Repository;
using Egeshka.Progress.Domain.Entities;
using Egeshka.Progress.Infrastructure.DataAccess.DbModels;
using Egeshka.Progress.Infrastructure.DataAccess.Mappers;
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
                streakItem.Type
            },
            cancellationToken: cancellationToken);

        return connection.ExecuteAsync(cmd);
    }

    public async Task<IReadOnlyCollection<StreakItem>> GetUserStreakItemsAsync(
        NpgsqlConnection connection,
        UserId userId,
        CancellationToken cancellationToken)
    {
        const string Sql =
            $"""
                select 
                    user_id "UserId",
                    date "Date",
                    type "Type"
                from {TableName}
                where user_id = @UserId
                order by date desc;
            """;

        var cmd = new CommandDefinition(
            Sql,
            new { UserId = userId.Value },
            cancellationToken: cancellationToken);

        var items = await connection.QueryAsync<StreakItemDbModel>(cmd);
        var result = items.Select(item => item.ToServiceModel()).ToArray();

        return result;
    }
}
