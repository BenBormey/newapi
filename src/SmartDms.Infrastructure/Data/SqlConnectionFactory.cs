using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace JuJuBis.Infrastructure.Data;

public interface ISqlConnectionFactory
{
    IDbConnection Create();
}

/// <summary>SQL Server connection factory</summary>
public sealed class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("Default")
            ?? throw new InvalidOperationException("Connection string 'Default' not found");
    }

    public IDbConnection Create() => new SqlConnection(_connectionString);
}
