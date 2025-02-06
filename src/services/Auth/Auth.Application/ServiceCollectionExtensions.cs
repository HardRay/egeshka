using Egeshka.Auth.Application.Options;
using Egeshka.Auth.Application.Services;
using Egeshka.Auth.Application.Services.Interfaces;
using Egeshka.Auth.Application.Services.TokenProviders;
using Egeshka.Auth.Application.Services.TokenProviders.Interfaces;
using Microsoft.Extensions.Configuration;
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
    public static IServiceCollection AddApplication(this IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        collection.AddTransient<IRegistrationTokenProvider, RegistrationTokenProvider>();
        collection.AddTransient<IAccessTokenProvider, AccessTokenProvider>();
        collection.AddTransient<IRefreshTokenProvider, RefreshTokenProvider>();
        collection.AddTransient<IDateTimeProvider, DateTimeProvider>();
        collection.AddTransient<IAuthorizationService, AuthorizationService>();

        collection.AddOptions<AccessTokenOptions>().Bind(configuration.GetSection(nameof(AccessTokenOptions)));

        var password = configuration.GetValue<string>("AccessTokenOptions:SecretKey");
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentException("Требуется добавить пароль к БД в переменную окружения AccessTokenOptions__SecretKey");
        }

        return collection;
    }
}
