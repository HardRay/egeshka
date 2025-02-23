using Dapper;
using Egeshka.Auth.Application.Models.Repository;
using Egeshka.Auth.Domain.Entities;
using Egeshka.Auth.Infrastructure.DataAccess.DbModels;
using Egeshka.Auth.Infrastructure.DataAccess.Mappers;
using Egeshka.Auth.Infrastructure.DataAccess.Repositories.Internal.Interfaces;
using Egeshka.Core.Application.Services.Interfaces;
using Egeshka.Core.Domain.ValueObjects;
using Npgsql;
using System.Text;

namespace Egeshka.Auth.Infrastructure.DataAccess.Repositories.Internal;

public sealed class UserInternalRepository(
    IDateTimeProvider dateTimeProvider) 
    : IUserInternalRepository
{
    private const string TableName = "users";
    private const string Fields =
        """
            id "Id",
            telegram_user_id "TelegramUserId",
            mobile_number "MobileNumber",
            first_name "FirstName",
            last_name "LastName"
        """;

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

    public async Task<User?> GetUserByMobileNumberAsync(
        NpgsqlConnection connection,
        MobileNumber mobileNumber,
        CancellationToken cancellationToken)
    {
        const string Sql =
            $"""
                select {Fields}
                from {TableName}
                where mobile_number = @MobileNumber
            """;

        var cmd = new CommandDefinition(
            Sql,
            new { MobileNumber = mobileNumber.Value },
            cancellationToken: cancellationToken);

        var dbModel = await connection.QueryFirstOrDefaultAsync<UserDbModel>(cmd);

        return dbModel?.ToServiceModel();
    }
}
