namespace Egeshka.AuthBot
{
    public class Startup(IConfiguration configuration)
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddEndpointsApiExplorer();
            serviceCollection.AddSwaggerGen();
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
                });
        }
    }
}
