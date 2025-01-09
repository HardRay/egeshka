using Egeshka.Auth.Application.Services;
using Egeshka.Auth.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Egeshka.Auth.Application;

/// <summary>
/// Расширения коллекции сервисов в DI для добавления сервисов Приложения
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавление сервисов Приложения
    /// </summary>
    public static IServiceCollection AddApplication(
        this IServiceCollection collection)
    {
        collection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        collection.AddTransient<IRegistrationTokenGenerator, RegistrationTokenGenerator>();
        collection.AddTransient<IDateTimeProvider, DateTimeProvider>();

        return collection;
    }
}
