using Lecture8.Models;

namespace Lecture8.DataProviders;

public interface ISqlProvider
{
    public IEnumerable<Producer> GetProducers(string defaultDb);
    public IEnumerable<ProducerJoin> GetProducersJoin(string defaultDb);
}