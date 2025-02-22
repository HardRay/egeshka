using Dapper;
using Egeshka.Auth.Application.Models.Repository;
using Egeshka.Auth.Application.Services.Interfaces;
using Egeshka.Auth.Infrastructure.DataAccess.Repositories.Internal.Interfaces;
using Npgsql;

namespace Egeshka.Auth.Infrastructure.DataAccess.Repositories.Internal;

public sealed class UserInternalRepository(
    IDateTimeProvider dateTimeProvider) 
    : IUserInternalRepository
{
    private const string TableName = "users";
    public Task<long> InsertUserAsync(NpgsqlConnection connection, UserInsertModel user, CancellationToken cancellationToken)
    {
        const string Sql =
            $"""
                insert into {TableName}(telegram_user_id, mobile_number, first_name, last_name, create_at, update_at)
                values (@TelegramUserId, @MobileNumber, @FirstName, @LastName, @CreateAt, @UpdateAt)
                returning id;
            """;

        var now = dateTimeProvider.UtcNow;
        var cmd = new CommandDefinition(
            Sql,
            new
            {
                TelegramUserId = user.TelegramUserId.Value,
                MobileNumber = user.MobileNumber.Value,
                user.FirstName,
                user.LastName,
                CreateAt = now,
                UpdateAt = now,
            },
            cancellationToken: cancellationToken);

        return connection.QueryFirstAsync<long>(cmd);
    }
}
