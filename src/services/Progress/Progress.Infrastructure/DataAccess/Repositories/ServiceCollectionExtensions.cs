using Egeshka.Progress.Infrastructure.DataAccess.Repositories.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Egeshka.Progress.Infrastructure.DataAccess.Repositories;

/// <summary>
/// Расширения коллекции сервисов в DI для добавления репозиториев
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавление репозиториев
    /// </summary>
    public static IServiceCollection AddRepositories(
        this IServiceCollection collection,
        string connectionString)
    {
        collection.AddSingleton<IPostgresConnectionFactory>(x => new PostgresConnectionFactory(connectionString));

        return collection;
    }
}
