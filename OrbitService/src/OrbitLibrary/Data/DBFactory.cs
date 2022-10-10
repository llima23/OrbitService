using System;
using System.Data;

namespace OrbitLibrary.Data
{
    public interface DBFactory
    {
        IDbConnection CreateConnection();
        IDbCommand CreateCommand();
        IDbDataAdapter CreateDataAdapter();
    }
}

