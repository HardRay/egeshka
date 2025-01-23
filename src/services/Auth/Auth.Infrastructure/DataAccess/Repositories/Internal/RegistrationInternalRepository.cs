using Dapper;
using Egeshka.Auth.Infrastructure.DataAccess.DbModels;
using Egeshka.Auth.Infrastructure.DataAccess.Repositories.Internal.Interfaces;
using Npgsql;
using System.Text;

namespace Egeshka.Auth.Infrastructure.DataAccess.Repositories.Internal;

public sealed class RegistrationInternalRepository : IRegistrationInternalRepository
{
    private const string TableName = "registrations";
    private const string Fields =
        """
            id "Id",
            telegram_user_id "TelegramUserId",
            mobile_number "MobileNumber",
            first_name "FirstName",
            last_name "LastName",
            registration_token "RegistrationToken",
            create_at "CreatedAt",
            update_at "UpdateAt"
        """;

    public Task<RegistrationDbModel?> GetByIdAsync(
        NpgsqlConnection connection, long id, CancellationToken cancellationToken, bool forUpdate = false)
    {
        const string Sql =
            $"""
                select {Fields}
                from {TableName}
                where id = @Id
            """;

        var sqlBuilder = new StringBuilder(Sql);
        if (forUpdate)
            sqlBuilder.AppendLine("for update");

        var cmd = new CommandDefinition(sqlBuilder.ToString(), new { Id = id }, cancellationToken: cancellationToken);

        return connection.QueryFirstOrDefaultAsync<RegistrationDbModel>(cmd);
    }

    public Task DeleteByIdAsync(NpgsqlConnection connection, long id, CancellationToken cancellationToken)
    {
        const string Sql =
            $"""
                delete
                from {TableName}
                where id = @Id
            """;

        var cmd = new CommandDefinition(Sql, new { Id = id }, cancellationToken: cancellationToken);

        return connection.ExecuteAsync(cmd);
    }
}
