using Microsoft.AspNetCore.Server.Kestrel.Core;
using Egeshka.AuthBot.Extensions;

namespace Egeshka.AuthBot;

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
        .Build()
        .RunAsync();
    }
}
