using Egeshka.Auth.Infrastructure.DataAccess.Migrations.Common;
using Egeshka.Auth.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Egeshka.Auth.Infrastructure.DataAccess;

/// <summary>
/// Расширения коллекции сервисов в DI для добавления DAL
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавление DAL
    /// </summary>
    public static IServiceCollection AddDataAccess(
        this IServiceCollection collection,
        IConfiguration configuration)
    {

        var connectionString = GetConnectionString(configuration);

        collection
            .AddFluentMigrator(connectionString)
            .AddRepositories(connectionString);

        return collection;
    }

    private static string GetConnectionString(IConfiguration configuration)
    {
        var baseConnectionString = configuration.GetValue<string>("AuthDatabase:ConnectionString");
        if (string.IsNullOrEmpty(baseConnectionString))
        {
            throw new ArgumentException("Требуется указать строку подключения к БД или она пустая");
        }

        var password = configuration.GetValue<string>("AuthDatabase:Password");
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentException("Требуется добавить пароль к БД в переменную окружения AuthDatabase__Password");
        }

        var builder = new NpgsqlConnectionStringBuilder(baseConnectionString)
        {
            Password = password
        };

        return builder.ConnectionString;
    }
}
