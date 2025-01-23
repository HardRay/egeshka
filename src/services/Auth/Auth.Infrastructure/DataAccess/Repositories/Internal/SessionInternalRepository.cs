using Dapper;
using Egeshka.Auth.Application.Models.Repository;
using Egeshka.Auth.Application.Services.Interfaces;
using Egeshka.Auth.Infrastructure.DataAccess.Repositories.Internal.Interfaces;
using Npgsql;

namespace Egeshka.Auth.Infrastructure.DataAccess.Repositories.Internal;

public sealed class SessionInternalRepository(
    IDateTimeProvider dateTimeProvider) 
    : ISessionInternalRepository
{
    private const string TableName = "sessions";

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
}
