using Lecture8.DataProviders;
using Lecture8.Models;
using Microsoft.Extensions.Hosting;
using NLog;

namespace Lecture8.Workers;

public class Lecture9Worker : BackgroundService
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly ISqlProvider _dataProvider;
    
    public Lecture9Worker(ISqlProvider dp)
    {
        _dataProvider = dp;
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.Trace("Lecture 9 Worker...");
        
        _logger.Trace("View");
        // IEnumerable<EnergyConsumption> res = _dataProvider.EnergyConsumptionView("Lab1");
        // res.ToList().ForEach(p =>
        // {
        //     _logger.Trace(p.ToString);
        // });
        
        
        // Calls AK, OH, KY using a stored procedure
        _logger.Trace("Stored Procedure");
        List<string> states = new List<string>
        {
            "AK",
            "OH",
            "KY"
        };

        // Stored procedure logic to loop over the list of states one by one
        foreach (string s in states)
        {
            IEnumerable<EnergyConsumption> res = _dataProvider.EnergyConsumptionByState("Lab1", s);
            res.ToList().ForEach(p =>
            {
                _logger.Trace(p.ToString);
            }); 
        }


        return Task.CompletedTask;
    }
}