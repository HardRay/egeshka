using Egeshka.ApiGateway.Extensions;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Egeshka.ApiGateway;

public class Program
{
    public async static Task Main(string[] args)
    {
        const string HTTP_PORT = "HTTP_PORT";

        await Host
            .CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(
                builder => builder.UseStartup<Startup>()
                    .ConfigureKestrel(
                        option =>
                        {
                            option.ListenPortByOptions(HTTP_PORT, HttpProtocols.Http1);
                        }))
            .ConfigureAppConfiguration(builder => builder.AddEnvironmentVariables())
            .Build()
            .RunAsync();
    }
}
