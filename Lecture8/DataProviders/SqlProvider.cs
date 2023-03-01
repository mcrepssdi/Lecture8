﻿using System.Data.SqlClient;
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
        _connectionStr = "Server=DESKTOP-IFN84LU\\SQLEXPRESS01;Database=Lab1;Trusted_Connection=True;";
    }

    public IEnumerable<Producer> GetProducers(string defaultDb)
    {
        Logger.Trace("Entering...");

        using SqlConnection conn = new (_connectionStr);
        try
        {
            conn.Open();
            conn.ChangeDatabase(defaultDb);
            IEnumerable<Producer> producers = conn.Query<Producer>("SELECT * FROM dbo.Producers");
            
            Logger.Trace($"{producers.Count()} producers found");
            return producers;
        }
        catch (Exception e)
        {
            Logger.Error(e.Message);
        }
        
        return new List<Producer>();
    }
}