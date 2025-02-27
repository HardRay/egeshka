using Egeshka.Progress.Hosting.Extensions;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Egeshka.Progress.Hosting;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        const string GRPC_PORT = "GRPC_PORT";

        await Host
            .CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(
                builder => builder.UseStartup<Startup>()
                    .ConfigureKestrel(
                        option =>
                        {
                            option.ListenPortByOptions(GRPC_PORT, HttpProtocols.Http2);
                        }))
            .ConfigureAppConfiguration(builder => builder.AddEnvironmentVariables())
            .Build()
            .RunOrMigrateAsync(args);
    }
}
