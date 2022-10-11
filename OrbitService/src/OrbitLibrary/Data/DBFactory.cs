using System;
using System.Data;

namespace OrbitLibrary.Data
{
    public interface DBFactory
    {
        public string DataBaseType { get; set; }
        public string DataBaseName { get; set; }
        IDbConnection CreateConnection();
        IDbCommand CreateCommand();
        IDbDataAdapter CreateDataAdapter();
    }
}

