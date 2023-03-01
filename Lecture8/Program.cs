using Lecture8.Configurations;
using Lecture8.Workers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;using NLog;



IHost host = Host.CreateDefaultBuilder(args)
    //.UseWindowServices()
    .ConfigureServices((hostContext, services) =>
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        logger.Trace("Starting Service");
        IUtConfiguration config = new StudentConfigurations();
        hostContext.Configuration.Bind("Config", config);
        logger.Info($"DB ConnStr: {config.AppEnvironment.ConnectionStr}");

        services.AddSingleton(config);
        
        
        services.AddHostedService<Shutdown>();
    })
    .Build();

await host.RunAsync();