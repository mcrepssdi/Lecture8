using Microsoft.Extensions.Hosting;
using NLog;

namespace Lecture8.Workers;

public class Shutdown : BackgroundService
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    
    public Shutdown(IHostApplicationLifetime hostApplicationLifetime)
    {
        _hostApplicationLifetime = hostApplicationLifetime;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Logger.Info($"Terminating Host Services at {DateTime.Now:yyyy-MM-dd HH:m:s tt zzz}");
            
        _hostApplicationLifetime.StopApplication();
        return Task.CompletedTask;
    }
}