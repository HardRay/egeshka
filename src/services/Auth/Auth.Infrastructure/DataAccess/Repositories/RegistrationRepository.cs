using Dapper;
using Egeshka.Auth.Application.Models;
using Egeshka.Auth.Application.Models.Repository;
using Egeshka.Auth.Application.Repository;
using Egeshka.Auth.Application.Services.Interfaces;
using Egeshka.Auth.Infrastructure.DataAccess.DbModels;
using Egeshka.Auth.Infrastructure.DataAccess.Mappers;
using Egeshka.Auth.Infrastructure.DataAccess.Repositories.Common;

namespace Egeshka.Auth.Infrastructure.DataAccess.Repositories;

public sealed class RegistrationRepository(
    IPostgresConnectionFactory connectionFactory,
    IDateTimeProvider dateTimeProvider)
    : IRegistrationRepository
{
    private const string TableName = "registrations";
    private const string Fields = "id, telegram_user_id, mobile_number, first_name, last_name, registration_token, create_at";

    public async Task<RegistrationModel?> GetByTokenAsync(string token, CancellationToken cancellationToken)
    {
        const string Sql =
            $"""
                select
                    id "Id",
                    telegram_user_id "TelegramUserId",
                    mobile_number "MobileNumber",
                    first_name "FirstName",
                    last_name "LastName",
                    registration_token "RegistrationToken",
                    create_at "CreatedAt"
                from {TableName}
                where registration_token = @Token
                for update;
            """;

        var cmd = new CommandDefinition(Sql, new { Token = token }, cancellationToken: cancellationToken);

        using var connection = connectionFactory.GetConnection();
        await connection.OpenAsync(cancellationToken);

        var registration = await connection.QueryFirstOrDefaultAsync<RegistrationDbModel>(cmd);

        return registration?.ToServiceModel();
    }

    public async Task InsertAsync(RegistrationInsertModel model, CancellationToken cancellationToken)
    {
        const string Sql =
            $"""
                insert into {TableName}(telegram_user_id, mobile_number, first_name, last_name, registration_token, create_at)
                values (@TelegramUserId, @MobileNumber, @FirstName, @LastName, @RegistrationToken, @CreateAt);
            """;

        var cmd = new CommandDefinition(
            Sql,
            new
            {
                TelegramUserId = model.TelegramUserId.Value,
                MobileNumber = model.MobileNumber.Value,
                model.FirstName,
                model.LastName,
                model.RegistrationToken,
                CreateAt = dateTimeProvider.UtcNow
            },
            cancellationToken: cancellationToken);

        using var connection = connectionFactory.GetConnection();
        await connection.ExecuteAsync(cmd);
    }
}
