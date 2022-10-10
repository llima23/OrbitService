using System;
using System.Data;

namespace OrbitLibrary.Data
{
    public interface IWrapper
    {
        public bool CanDbConnect();

        public int ExecuteNonQuery(string commandString);

        public int ExecuteNonQueryCommand(IDbCommand command);

        public DataSet ExecuteQuery(string queryString);

        public DataSet ExecuteQueryCommand(IDbCommand command);
    }
}
