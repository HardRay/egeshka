using Egeshka.Core.Hosting.Interceptors;
using Egeshka.Progress.Hosting.GrpcServices;
using Egeshka.Progress.Infrastructure;

namespace Egeshka.Progress.Hosting;

public sealed class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddInfrastructure(configuration);

        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen();

        serviceCollection.AddGrpc(options =>
        {
            options.Interceptors.Add<ExceptionsHandlingInterceptor>();
            options.EnableDetailedErrors = true;
        });
        serviceCollection.AddGrpcReflection();
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
            });
    }
}