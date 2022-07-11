using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Cinema.Mediator;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseMetricsWebTracking()
            .UseMetrics(options =>
            {
                options.EndpointOptions = endpoints =>
                {
                    endpoints.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
                    endpoints.MetricsEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();
                    endpoints.EnvironmentInfoEndpointEnabled = false;
                };
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Environment.CurrentDirectory)
                    .AddJsonFile("appsettings.json", optional: false, true)
                    .AddJsonFile("appsettings.Development.json", optional: true, true)
                    .Build();

                webBuilder
                    .UseConfiguration(config)
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseKestrel()
                    .UseUrls(config.GetSection("Url").Value)
                    .UseIISIntegration()
                    .UseStartup<Startup>();
            });
}