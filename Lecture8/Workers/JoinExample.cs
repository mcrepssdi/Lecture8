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

        IEnumerable<ProducerJoin> joins = _dataProvider.GetProducersJoin("Lab1");
        joins.ToList().ForEach(p =>
        {
            _logger.Trace($"ID: {p.Id}, Type: {p.Type_Of_Producer}, BID: {p.Bid}, BTYPE: {p.BType}");
        });

        IEnumerable<string> distinctItems = _dataProvider.DistinctSeries("Lab1");
        distinctItems.ToList().ForEach(p =>
        {
            _logger.Trace($"Value: {p}");
        });
        
        IEnumerable<DistinctValues> distinctValues = _dataProvider.DistinctValues("Lab1");
        distinctValues.ToList().ForEach(p =>
        {
            _logger.Trace($"Series: {p.Series_reference}, Period: {p.Period}, Date: {p.LastUpdated}");
        });

        // Build what needs to be inserted
        Series series = new()
        {
            Series_reference = "PRDA.S1CAAZI",
            Period = 1978.03,
            Data_value = "2550",
            status = "FINAL",
            Units = "Index",
            Magntude = "0",
            Subject = "Productivity Statistics - PRD",
            Group = "Productivity Indexes - Industry Level (ANZSIC06)",
            Series_title_1 = "Capital",
            Series_title_2 = "Agriculture, Forestry and Fishing",
            Series_title_3 = "Index",
            Series_title_4 = "",
            Series_title_5 = "",
            LastUpdated = DateTime.Now
        };
        int rows = _dataProvider.InsertNewSeriesValue("Lab1", series);
        _logger.Trace($"Rows Inserted: {rows}");
        
        rows = _dataProvider.DeleteNewSeriesValue("Lab1", "2750",  1978.03);
        _logger.Trace($"Rows Deleted: {rows}");
        
        return Task.CompletedTask;
    }
    
}