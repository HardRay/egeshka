using Egeshka.Core.Hosting.Extensions;
using Egeshka.Core.Hosting.Interceptors;
using Egeshka.Progress.Hosting.GrpcServices;
using Egeshka.Progress.Infrastructure;

namespace Egeshka.Progress.Hosting;

public sealed class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        var assembly = typeof(Startup).Assembly;

        services
            .AddInfrastructure(configuration);

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddGrpc(options =>
        {
            options.Interceptors.Add<ExceptionsHandlingInterceptor>();
            options.Interceptors.Add<ValidationInterceptor>();
            options.EnableDetailedErrors = true;
        });
        services.AddGrpcReflection();

        services.AddValidatorsFromAssembly(assembly);
    }

    public void Configure(IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseRouting();
        applicationBuilder.UseSwagger();
        applicationBuilder.UseSwaggerUI();

        applicationBuilder.UseEndpoints(
            endpointRouteBuilder =>
            {
                endpointRouteBuilder.MapGrpcReflectionService();
                endpointRouteBuilder.MapGrpcService<ProgressGrpcService>();
                endpointRouteBuilder.MapGrpcService<StreakGprcService>();
            });
    }
}