using Microsoft.Data.SqlClient;
using SQLServer.DatabaseContext;
using System.Data;

namespace SQLServer.Repository.Base;

public abstract class BaseRepository : IDisposable
{
    protected readonly DBContext _dbContext;
    private bool _disposed = false;

    protected BaseRepository(DBContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected SqlCommand CreateCommand(string query, params SqlParameter[] parameters)
    {
        var command = _dbContext.GetConnection().CreateCommand();
        command.CommandText = query;
        command.Parameters.AddRange(parameters);
        return command;
    }

    protected int ExecuteNonQuery(string query, params SqlParameter[] parameters)
    {
        using var command = CreateCommand(query, parameters);
        return command.ExecuteNonQuery();
    }

    protected object ExecuteScalar(string query, params SqlParameter[] parameters)
    {
        using var command = CreateCommand(query, parameters);
        return command.ExecuteScalar();
    }

    protected SqlDataReader ExecuteReader(string query, params SqlParameter[] parameters)
    {
        var command = CreateCommand(query, parameters);
        return command.ExecuteReader(CommandBehavior.CloseConnection);
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
                _dbContext?.Dispose();
            }
            _disposed = true;
        }
    }
}