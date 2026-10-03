
using RDATA.Dapper.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace RDATA.Dapper.Implementations
{
    public class SqlConnectionHelper: ISqlConnectionHelper
    {
        private readonly string _connectionString;
        private IDbConnection _connection;
        public SqlConnectionHelper(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IDbConnection GetDbConnection()
        {
            if (_connection == null || _connection.State != ConnectionState.Open)
            {
                _connection = new SqlConnection(_connectionString);
                _connection.Open();
            }
            return _connection;
        }
        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Dispose();
            }
        }
    }
}