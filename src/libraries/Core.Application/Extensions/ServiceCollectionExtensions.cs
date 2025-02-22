using Egeshka.Core.Application.Services.Interfaces;
using Egeshka.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Egeshka.Core.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}
