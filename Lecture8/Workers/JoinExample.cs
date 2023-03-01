using Lecture8.DataProviders;
using Lecture8.Models;
using Microsoft.Extensions.Hosting;
using NLog;

namespace Lecture8.Workers;

public class JoinExample : BackgroundService
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly ISqlProvider _dataProvider;
    
    public JoinExample(ISqlProvider dp)
    {
        _dataProvider = dp;
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        IEnumerable<Producer> producers = _dataProvider.GetProducers("Lab1");
        producers.ToList().ForEach(p =>
        {
            _logger.Trace($"ID: {p.Id}\tType: {p.Type_Of_Producer}");
        });
        
        return Task.CompletedTask;
    }
}