using Dapper;
using Egeshka.Auth.Application.Models;
using Egeshka.Auth.Application.Models.Repository;
using Egeshka.Auth.Application.Repository;
using Egeshka.Auth.Application.Services.Interfaces;
using Egeshka.Auth.Domain.Entities;
using Egeshka.Auth.Infrastructure.DataAccess.DbModels;
using Egeshka.Auth.Infrastructure.DataAccess.Mappers;
using Egeshka.Auth.Infrastructure.DataAccess.Repositories.Common;
using Egeshka.Auth.Infrastructure.DataAccess.Repositories.Internal.Interfaces;

namespace Egeshka.Auth.Infrastructure.DataAccess.Repositories;

public sealed class RegistrationRepository(
    IPostgresConnectionFactory connectionFactory,
    IRegistrationInternalRepository internalRepository,
    IDateTimeProvider dateTimeProvider)
    : IRegistrationRepository
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

    public async Task InsertAsync(RegistrationInsertModel model, CancellationToken cancellationToken)
    {
        const string Sql =
            $"""
                insert into {TableName}(telegram_user_id, mobile_number, first_name, last_name, registration_token, create_at, update_at)
                values (@TelegramUserId, @MobileNumber, @FirstName, @LastName, @RegistrationToken, @CreateAt, @UpdateAt);
            """;

        var now = dateTimeProvider.UtcNow;
        var cmd = new CommandDefinition(
            Sql,
            new
            {
                TelegramUserId = model.TelegramUserId.Value,
                MobileNumber = model.MobileNumber.Value,
                model.FirstName,
                model.LastName,
                model.RegistrationToken,
                CreateAt = now,
                UpdateAt = now,
            },
            cancellationToken: cancellationToken);

        using var connection = connectionFactory.GetConnection();
        await connection.ExecuteAsync(cmd);
    }

    public async Task<Registration?> GetByTokenAsync(string token, CancellationToken cancellationToken)
    {
        const string Sql =
            $"""
                select {Fields}
                from {TableName}
                where registration_token = @Token;
            """;

        var cmd = new CommandDefinition(Sql, new { Token = token }, cancellationToken: cancellationToken);

        using var connection = connectionFactory.GetConnection();
        await connection.OpenAsync(cancellationToken);

        var registration = await connection.QueryFirstOrDefaultAsync<RegistrationDbModel>(cmd);

        return registration?.ToServiceModel();
    }

    public async Task<Registration?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        using var connection = connectionFactory.GetConnection();
        await connection.OpenAsync(cancellationToken);

        var registration = await internalRepository.GetByIdAsync(connection, id, cancellationToken);

        return registration?.ToServiceModel();
    }
}
