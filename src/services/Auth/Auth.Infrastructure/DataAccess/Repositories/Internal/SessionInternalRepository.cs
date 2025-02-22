using Dapper;
using Egeshka.Auth.Application.Models.Repository;
using Egeshka.Auth.Infrastructure.DataAccess.DbModels;
using Egeshka.Auth.Infrastructure.DataAccess.Repositories.Internal.Interfaces;
using Egeshka.Core.Application.Services.Interfaces;
using Npgsql;
using System.Text;

namespace Egeshka.Auth.Infrastructure.DataAccess.Repositories.Internal;

public sealed class SessionInternalRepository(
    IDateTimeProvider dateTimeProvider) 
    : ISessionInternalRepository
{
    private const string TableName = "sessions";
    private const string Fields =
        """
            id "Id",
            user_id "UserId",
            access_token "AccessToken",
            refresh_token "RefreshToken",
            create_at "CreatedAt",
            update_at "UpdateAt"
        """;

    public Task<SessionDbModel?> GetByRefreshTokenAsync(
        NpgsqlConnection connection,
        string refreshToken,
        CancellationToken cancellationToken,
        bool forUpdate = false)
    {
        const string Sql =
            $"""
                select {Fields}
                from {TableName}
                where refresh_token = @RefreshToken
            """;

        var sqlBuilder = new StringBuilder(Sql);
        if (forUpdate)
            sqlBuilder.AppendLine("for update");

        var cmd = new CommandDefinition(
            sqlBuilder.ToString(),
            new { RefreshToken = refreshToken },
            cancellationToken: cancellationToken);

        return connection.QueryFirstOrDefaultAsync<SessionDbModel>(cmd);
    }

    public Task InsertAsync(NpgsqlConnection connection, SessionInsertModel session, CancellationToken cancellationToken)
    {
        const string Sql =
            $"""
                insert into {TableName}(user_id, access_token, refresh_token, create_at, update_at)
                values (@UserId, @AccessToken, @RefreshToken, @CreateAt, @UpdateAt);
            """;

        var now = dateTimeProvider.UtcNow;
        var cmd = new CommandDefinition(
            Sql,
            new
            {
                session.UserId,
                session.AccessToken,
                session.RefreshToken,
                CreateAt = now,
                UpdateAt = now,
            },
            cancellationToken: cancellationToken);

        return connection.ExecuteAsync(cmd);
    }

    public Task UpdateSession(NpgsqlConnection connection, SessionUpdateModel session, CancellationToken cancellationToken)
    {
        const string Sql =
            $"""
                update {TableName}
                set
                    access_token = @AccessToken,
                    refresh_token = @RefreshToken,
                    update_at = @UpdateAt
                where id = @Id;
            """;
        
        var cmd = new CommandDefinition(
            Sql,
            new
            {
                session.Id,
                session.AccessToken,
                session.RefreshToken,
                UpdateAt = dateTimeProvider.UtcNow,
            },
            cancellationToken: cancellationToken);

        return connection.ExecuteAsync(cmd);
    }
}
