using Lecture8.Models;

namespace Lecture8.DataProviders;

public interface ISqlProvider
{
    public IEnumerable<Producer> GetProducers(string defaultDb);
    public IEnumerable<ProducerJoin> GetProducersJoin(string defaultDb);
    public IEnumerable<string> DistinctSeries(string defaultDb);
    public IEnumerable<DistinctValues> DistinctValues(string defaultDb);
    public int InsertNewSeriesValue(string defaultDb, Series series);
    public int DeleteNewSeriesValue(string defaultDb, string keyValue, double period);
}