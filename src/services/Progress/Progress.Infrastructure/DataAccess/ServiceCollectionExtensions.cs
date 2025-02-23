using Egeshka.Progress.Infrastructure.DataAccess.Migrations.Common;
using Egeshka.Progress.Infrastructure.DataAccess.Repositories.Common;
using Egeshka.Progress.Infrastructure.DataAccess.TypeHandlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Egeshka.Progress.Infrastructure.DataAccess;

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

        AddTypeHandlers();

        return collection;
    }

    private static string GetConnectionString(IConfiguration configuration)
    {
        var baseConnectionString = configuration.GetValue<string>("ProgressDatabase:ConnectionString");
        if (string.IsNullOrEmpty(baseConnectionString))
        {
            throw new ArgumentException("Требуется указать строку подключения к БД или она пустая");
        }

        var password = configuration.GetValue<string>("ProgressDatabase:Password");
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentException("Требуется добавить пароль к БД в переменную окружения ProgressDatabase__Password");
        }

        var builder = new NpgsqlConnectionStringBuilder(baseConnectionString)
        {
            Password = password
        };

        return builder.ConnectionString;
    }

    private static void AddTypeHandlers()
    {
        Dapper.SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
    }
}
