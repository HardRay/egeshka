using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Egeshka.Progress.Application;

/// <summary>
/// Расширения коллекции сервисов в DI для добавления сервисов Приложения
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавление сервисов Приложения
    /// </summary>
    public static IServiceCollection AddApplication(this IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return collection;
    }
}
