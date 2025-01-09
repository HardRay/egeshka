using Dapper;
using Egeshka.Auth.Application.Models;
using Egeshka.Auth.Application.Repository;
using Egeshka.Auth.Application.Services.Interfaces;
using Egeshka.Auth.Infrastructure.DataAccess.Repositories.Common;

namespace Egeshka.Auth.Infrastructure.DataAccess.Repositories;

public sealed class RegistrationRepository(
    IPostgresConnectionFactory connectionFactory,
    IDateTimeProvider dateTimeProvider)
    : IRegistrationRepository
{
    private const string TableName = "registrations";
    public async Task RegistrationAsync(RegistrationModel model, CancellationToken cancellationToken)
    {
        const string Sql =
            $"""
                insert into {TableName}(telegram_user_id, mobile_number, first_name, last_name, create_at)
                values (@TelegramUserId, @MobileNumber, @FirstName, @LastName, @CreateAt);
            """;

        var cmd = new CommandDefinition(
            Sql,
            new
            {
                TelegramUserId = model.TelegramUserId.Value,
                MobileNumber = model.MobileNumber.Value,
                model.FirstName,
                model.LastName,
                CreateAt = dateTimeProvider.UtcNow
            },
            cancellationToken: cancellationToken);

        using var connection = connectionFactory.GetConnection();
        await connection.ExecuteAsync(cmd);
    }
}
