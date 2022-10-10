using System;
using System.Data;
using System.Data.Odbc;
using OrbitLibrary.Data;

namespace OrbitLibrary.Infrastructure.Data
{
    public class ODBCDbFactory: DBFactory
    {
        private string connectionString;
        public ODBCDbFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbCommand CreateCommand()
        {
            return new OdbcCommand();
        }

        public IDbConnection CreateConnection()
        {
            OdbcConnection connection = new OdbcConnection(connectionString);
            return connection;
        }

        public IDbDataAdapter CreateDataAdapter()
        {
            return new OdbcDataAdapter();
        }
    }
}
