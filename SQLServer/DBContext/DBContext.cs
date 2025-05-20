using Microsoft.Data.SqlClient;
using System.Data;
using Core.Core.Config;

namespace SQLServer.DatabaseContext;

public class DBContext : IDisposable
{
    private readonly SqlConnection _sqlConnection;
    private bool _disposed = false;

    public DBContext()
    {
        _sqlConnection = new SqlConnection(Config.ConnectionString);
    }

    public void OpenConnection()
    {
        if (_sqlConnection.State != ConnectionState.Open)
        {
            _sqlConnection.Open();
        }
    }

    public void CloseConnection()
    {
        if (_sqlConnection.State != ConnectionState.Closed)
        {
            _sqlConnection.Close();
        }
    }

    public SqlConnection GetConnection()
    {
        if (_sqlConnection.State != ConnectionState.Open)
            OpenConnection();

        return _sqlConnection;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                CloseConnection();
                _sqlConnection?.Dispose();
            }
            _disposed = true;
        }
    }
}