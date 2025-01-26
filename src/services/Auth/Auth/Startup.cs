using Egeshka.Auth.GrpcServices;
using Egeshka.Auth.Infrastructure;
using Egeshka.Core.Hosting.Interceptors;

namespace Egeshka.Auth;

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
                endpointRouteBuilder.MapGet("", () => "Hello Wold!");
                endpointRouteBuilder.MapGrpcReflectionService();
                endpointRouteBuilder.MapGrpcService<AuthGrpcService>();
            });
    }
}