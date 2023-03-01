using System.Data.SqlClient;
using System.Text;
using Dapper;
using Lecture8.Models;
using NLog;

namespace Lecture8.DataProviders;

public class SqlProvider : ISqlProvider
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly string _connectionStr;
    public SqlProvider(string connectionStr)
    {
        _connectionStr = connectionStr;
    }

    public IEnumerable<Producer> GetProducers(string defaultDb)
    {
        Logger.Trace("Entering...");

        const string sql = "SELECT * FROM dbo.Producers WHERE Id > @Value";
        DynamicParameters dp = new();
        dp.Add("@Value", "5");
        
        using SqlConnection conn = new (_connectionStr);
        try
        {
            conn.Open();
            conn.ChangeDatabase(defaultDb);
            IEnumerable<Producer> producers = conn.Query<Producer>(sql, dp);
            
            Logger.Trace($"{producers.Count()} producers found");
            return producers;
        }
        catch (Exception e)
        {
            Logger.Error(e.Message);
        }
        
        return new List<Producer>();
    }

    public IEnumerable<ProducerJoin> GetProducersJoin(string defaultDb)
    {
        Logger.Trace("Entering...");

        StringBuilder sb = new();
        sb.AppendLine("SELECT A.*, B.Id AS BID, B.TYPE_OF_PRODUCER AS BTYPE ");
        sb.AppendLine("FROM dbo.Producers A ");
        sb.AppendLine("JOIN dbo.Producers_Temp B ");
        sb.AppendLine("ON A.ID = B.Id ");

        using SqlConnection conn = new (_connectionStr);
        try
        {
            conn.Open();
            conn.ChangeDatabase(defaultDb);
            IEnumerable<ProducerJoin> producers = conn.Query<ProducerJoin>(sb.ToString());
            
            Logger.Trace($"{producers.Count()} producers found");
            return producers;
        }
        catch (Exception e)
        {
            Logger.Error(e.Message);
        }
        
        return new List<ProducerJoin>();
    }
}