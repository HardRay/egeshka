using Egeshka.Auth.Infrastructure;

namespace Egeshka.Auth;

public sealed class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddInfrastructure(configuration);

        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseRouting();
        applicationBuilder.UseSwagger();
        applicationBuilder.UseSwaggerUI();

        //applicationBuilder.UseEndpoints(
        //    endpointRouteBuilder =>
        //    {
        //        endpointRouteBuilder.MapGrpcReflectionService();
        //    });
    }
}