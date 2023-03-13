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

    public IEnumerable<string> DistinctSeries(string defaultDb)
    {
        Logger.Trace("Entering...");

        StringBuilder sb = new();
        sb.AppendLine("SELECT DISTINCT Series_reference ");
        sb.AppendLine("FROM [dbo].[productivity-statistics-1978-2020-csv] ");
        sb.AppendLine("ORDER BY Series_reference ");

        using SqlConnection conn = new (_connectionStr);
        try
        {
            conn.Open();
            conn.ChangeDatabase(defaultDb);
            IEnumerable<string> distinctValues = conn.Query<string>(sb.ToString());
            
            Logger.Trace($"{distinctValues.Count()} distinct values");
            return distinctValues;
        }
        catch (Exception e)
        {
            Logger.Error(e.Message);
        }
        
        return new List<string>();
    }

    public IEnumerable<DistinctValues> DistinctValues(string defaultDb)
    {
        Logger.Trace("Entering...");

        StringBuilder sb = new();
        sb.AppendLine("SELECT DISTINCT Series_reference, Period, LastUpdated ");
        sb.AppendLine("FROM [dbo].[productivity-statistics-1978-2020-csv] ");
        sb.AppendLine("ORDER BY Series_reference ");

        using SqlConnection conn = new (_connectionStr);
        try
        {
            conn.Open();
            conn.ChangeDatabase(defaultDb);
            IEnumerable<DistinctValues> distinctValues = conn.Query<DistinctValues>(sb.ToString());
            
            Logger.Trace($"{distinctValues.Count()} distinct values");
            return distinctValues;
        }
        catch (Exception e)
        {
            Logger.Error(e.Message);
        }
        
        return new List<DistinctValues>();
    }

    public int InsertNewSeriesValue(string defaultDb, Series series)
    {
        Logger.Trace("Entering...");
        
        StringBuilder sb = new();
        sb.AppendLine("INSERT INTO [dbo].[productivity-statistics-1978-2020-csv] ");
        sb.AppendLine("(Series_reference,Period,Data_value,STATUS ");
        sb.AppendLine(",UNITS,MAGNTUDE,Subject,[Group],Series_title_1 ");
        sb.AppendLine(",Series_title_2,Series_title_3,Series_title_4,Series_title_5,LastUpdated) ");
        sb.AppendLine(" VALUES ");
        sb.AppendLine("(@Series_reference,@Period,@Data_value,@STATUS ");
        sb.AppendLine(",@UNITS,@MAGNTUDE,@Subject,@Group,@Series_title_1 ");
        sb.AppendLine(",@Series_title_2,@Series_title_3,@Series_title_4,@Series_title_5,@LastUpdated) ");
        
        DynamicParameters dp = new();
        dp.Add("@Series_reference", series.Series_reference);
        dp.Add("@Period", series.Period);
        dp.Add("@Data_value", series.Data_value);
        dp.Add("@STATUS", series.status);
        dp.Add("@UNITS", series.Units);
        dp.Add("@MAGNTUDE", series.Magntude);
        dp.Add("@Subject", series.Subject);
        dp.Add("@Group", series.Group);
        dp.Add("@Series_title_1", series.Series_title_1);
        dp.Add("@Series_title_2", series.Series_title_2);
        dp.Add("@Series_title_3", series.Series_title_3);
        dp.Add("@Series_title_4", series.Series_title_4);
        dp.Add("@Series_title_5", series.Series_title_5);
        dp.Add("@LastUpdated", series.LastUpdated);
        
        using SqlConnection conn = new (_connectionStr);
        try
        {
            conn.Open();
            conn.ChangeDatabase(defaultDb);
            int rows  = conn.Execute(sb.ToString(), dp);

            if (rows > 1)
            {
                throw new Exception($"Rows returned is greater than 1.  Rows :{rows}");
            }
            
            Logger.Trace($"{rows} inserted");
            return rows;
        }
        catch (Exception e)
        {
            Logger.Error(e.Message);
        }
        
        return int.MinValue;
    }

    public int DeleteNewSeriesValue(string defaultDb, string keyValue, double period)
    {
        Logger.Trace("Entering...");
        
        StringBuilder sb = new();
        sb.AppendLine("DELETE FROM [dbo].[productivity-statistics-1978-2020-csv] ");
        sb.AppendLine("WHERE Data_value = @value AND Period = @Period ");

        DynamicParameters dp = new();
        dp.Add("@value", keyValue);
        dp.Add("@Period", period);

        using SqlConnection conn = new (_connectionStr);
        try
        {
            conn.Open();
            conn.ChangeDatabase(defaultDb);
            int rows  = conn.Execute(sb.ToString(), dp);
            
            Logger.Trace($"{rows} deleted");
            return rows;
        }
        catch (Exception e)
        {
            Logger.Error(e.Message);
        }
        
        return int.MinValue;
    }
}