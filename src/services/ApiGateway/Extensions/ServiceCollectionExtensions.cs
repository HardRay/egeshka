using Egeshka.ApiGateway.ExceptionHandlers;
using Egeshka.ApiGateway.Options;
using Egeshka.ApiGateway.Providers;
using Egeshka.ApiGateway.Providers.Interfaces;
using Egeshka.Grpc.Auth;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace Egeshka.ApiGateway.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddTransient<IAuthProvider, AuthProvider>();

        return services;
    }

    public static IServiceCollection ConfigureAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var authOptions = configuration.GetSection(nameof(AuthOptions)).Get<AuthOptions>()
            ?? throw new InvalidOperationException("AuthOptions is null. Check configuration");

        services
            .AddAuthorization()
            .AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    options.Authority = authOptions.Issuer;
                    options.Audience = authOptions.Audience;
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.MapInboundClaims = false;
                    options.MapInboundClaims = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        RequireAudience = false,
                        ValidateAudience = false,
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidIssuer = authOptions.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.SecretKey))
                    };
                });


        return services;
    }

    public static IServiceCollection AddExceptionHandlers(
        this IServiceCollection services)
    {
        services.AddExceptionHandler<RpcExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }

    public static IServiceCollection ConfigureGrpc(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddGrpc();
        services.AddGrpcReflection();
        services.AddGrpcClients(configuration);

        return services;
    }

    private static IServiceCollection AddGrpcClients(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddGrpcClient<AuthGrpc.AuthGrpcClient>(options =>
        {
            var url = configuration.GetValue<string>("AUTH_ADDRESS");

            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("Требуется указать переменную окружения AUTH_ADDRESS или она пустая");
            }

            options.Address = new Uri(url);
        });

        return services;
    }

    public static IServiceCollection ConfigureSwagger(
        this IServiceCollection services)
    {
        services.AddSwaggerGen(setup =>
        {
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                Scheme = "bearer",
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Description = "Please Put **ONLY** JWT Bearer token",
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            };

            setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
            setup.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                { jwtSecurityScheme, Array.Empty<string>() }
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            setup.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        return services;
    }
}