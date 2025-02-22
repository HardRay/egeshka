using Egeshka.Auth.GrpcServices;
using Egeshka.Auth.Infrastructure;
using Egeshka.Core.Hosting.Extensions;
using Egeshka.Core.Hosting.Interceptors;
using FluentValidation;

namespace Egeshka.Auth;

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
                endpointRouteBuilder.MapGet("", () => "Hello Wold!");
                endpointRouteBuilder.MapGrpcReflectionService();
                endpointRouteBuilder.MapGrpcService<AuthGrpcService>();
            });
    }
}