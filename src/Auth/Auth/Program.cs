using Microsoft.AspNetCore.Server.Kestrel.Core;
using Egeshka.Auth.Extensions;

namespace Egeshka.Auth;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        const string GRPC_PORT = "GRPC_PORT";
        const string HTTP_PORT = "HTTP_PORT";

        await Host
            .CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(
                builder => builder.UseStartup<Startup>()
                    .ConfigureKestrel(
                        option =>
                        {
                            option.ListenPortByOptions(GRPC_PORT, HttpProtocols.Http2);
                            option.ListenPortByOptions(HTTP_PORT, HttpProtocols.Http1);
                        }))
            .Build()
            .RunOrMigrateAsync(args);
    }
}
