using System.Reflection;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Cinema.Mediator;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

public class Program
{
    public static void Main(string[] args)
    {
        //configure logging first
        ConfigureLogging();

        //then create the host, so that if the host fails we can log errors
        CreateHost(args);
        CreateHostBuilder(args).Build().Run();
    }
    
    private static void CreateHost(string[] args)
    {
        try
        {
            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        {
            Log.Fatal($"Failed to start {Assembly.GetExecutingAssembly().GetName().Name}", ex);
            throw;
        }
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
            })
            .UseSerilog();
    
    private static void ConfigureLogging()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile(
                $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                optional: true)
            .Build();

        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithMachineName()
            .WriteTo.Debug()
            .WriteTo.Console()
            .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
            .Enrich.WithProperty("Environment", environment)
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }
    
    private static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
    {
        return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
        {
            AutoRegisterTemplate = true,
            IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
        };
    }
}