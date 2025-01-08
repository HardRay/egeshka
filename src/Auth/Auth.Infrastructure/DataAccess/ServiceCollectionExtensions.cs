using Egeshka.Auth.Infrastructure.DataAccess.Migrations.Common;
using Egeshka.Auth.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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
        var connectionString = configuration.GetValue<string>("ConnectionStrings:OrdersDatabase");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException("Требуется указать строку подключения к БД или она пустая");
        }

        collection
            .AddFluentMigrator(connectionString)
            .AddRepositories(connectionString);

        return collection;
    }
}
