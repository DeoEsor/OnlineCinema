using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using OnlineCinema.DAL;
using OnlineCinema.DAL.Core;
using OnlineCinema.DAL.Users;

namespace Cinema.Mediator;

public class Startup
{
    public static IConfiguration Configuration { get; set; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }
        
    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<KestrelServerOptions>(o => o.AllowSynchronousIO = true);
        services.AddMetrics();
        services.AddLogging();
        Config(services, Configuration);
        MigrateDatabase(Configuration,services.BuildServiceProvider());
    }
    
    internal IServiceCollection Config( IServiceCollection services, IConfiguration configuration)
    {
        DbConfig(services, configuration);
        GRpcConfig(services);

        return services;
    }
    
    internal IServiceCollection GRpcConfig(IServiceCollection services)
    {
        services.AddGrpc();
        return services;
    }
    
    internal IServiceCollection MapGRpc(IServiceCollection app)
    {
        //app.MapGrpcService<>();
        return app;
    }
    
    internal IServiceCollection DbConfig(IServiceCollection services, IConfiguration configuration)
    {
        UsersDbConfig( services, configuration);
        FilmDbConfig(services, configuration);
        services.AddSingleton<UnitOfWork<UsersContext>>();
        services.AddSingleton<UnitOfWork<FilmsContext>>();
        services.AddSingleton<EfUsers>();
        services.AddSingleton<EfFilms>();
        return services;
    }
    
    internal IServiceCollection UsersDbConfig(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UsersContext>(
            options =>  
                options
                    .UseSqlServer(configuration.GetConnectionString("UserDBConnection")));
        return services;
    }
    
    internal IServiceCollection FilmDbConfig(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FilmsContext>(
            options =>  
                options
                    .UseSqlServer(configuration.GetConnectionString("FilmsDBConnection")));
        
        return services;
    }

        
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            //endpoints.MapGrpcService<UsersService>();
        });
    }

    public void MigrateDatabase(IConfiguration configuration, IServiceProvider provider)
    {
        configuration.GetConnectionString("UserDBConnection");
        provider.GetService<UsersContext>()
            ?.Database.Migrate();
        configuration.GetConnectionString("FilmsDBConnection");
        provider.GetService<FilmsContext>()
            ?.Database.Migrate();
    }
}