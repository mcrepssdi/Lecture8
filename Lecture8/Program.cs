using Lecture8.Configurations;
using Lecture8.Workers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        Console.WriteLine("Hello, World!");
        IUtConfiguration config = new StudentConfigurations();
        hostContext.Configuration.Bind("Config", config);
        
        services.AddHostedService<Shutdown>();
    })
    .Build();

await host.RunAsync();