using Egeshka.Progress.Application;
using Egeshka.Progress.Infrastructure.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Egeshka.Progress.Infrastructure;

/// <summary>
/// Расширения коллекции сервисов в DI для добавления сервисов Инфраструктуры
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавление сервисов Инфраструктуры
    /// </summary>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection collection,
        IConfiguration configuration)
    {
        collection
            .AddDataAccess(configuration)
            .AddApplication(configuration);

        return collection;
    }
}