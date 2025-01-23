using Egeshka.Auth.Application;
using Egeshka.Auth.Infrastructure.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Egeshka.Auth.Infrastructure;

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
            .AddApplication();

        return collection;
    }
}