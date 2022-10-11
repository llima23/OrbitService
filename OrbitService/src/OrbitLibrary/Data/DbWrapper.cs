using System;
using System.Data;

namespace OrbitLibrary.Data
{
    public class DbWrapper : IWrapper
    {
        public const int COMMAND_TIMEOUT = 600;
  
        private DBFactory factory;
        public string DataBaseType { get; set; }
        public string DataBaseName { get; set; }

        public DbWrapper(DBFactory factory)
        {
            this.factory = factory;
            this.DataBaseName = factory.DataBaseName;
            this.DataBaseType = factory.DataBaseType;
        }
  

        public bool CanDbConnect()
        {
            IDbConnection connection = factory.CreateConnection();
            try
            {
                connection.Open();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public int ExecuteNonQuery(string commandString)
        {
            IDbCommand command = factory.CreateCommand();
            command.CommandText = commandString;
            return ExecuteNonQueryCommand(command);
        }

        public int ExecuteNonQueryCommand(IDbCommand command)
        {
            if (command == null)
            {
                throw new ArgumentException("The command object should be a valid instance");
            }

            IDbConnection connection = factory.CreateConnection();
            connection.Open();
            command.Connection = connection;
            command.CommandTimeout = COMMAND_TIMEOUT;
            int numberRowsAffected = command.ExecuteNonQuery();
            connection.Close();
            return numberRowsAffected;

        }

        public DataSet ExecuteQuery(string queryString)
        {
            IDbCommand queryCommand = factory.CreateCommand();
            queryCommand.CommandText = queryString;
            return ExecuteQueryCommand(queryCommand);
        }

        public DataSet ExecuteQueryCommand(IDbCommand command)
        {
            if (command == null)
            {
                throw new ArgumentException("The command object should be a valid instance");
            }

            DataSet resultDataSet = new DataSet();

            try
            {
                IDbConnection connection = factory.CreateConnection();
                IDbDataAdapter dataAdapter = factory.CreateDataAdapter();

                connection.Open();
                command.Connection = connection;
                command.CommandTimeout = COMMAND_TIMEOUT;
                dataAdapter.SelectCommand = command;
                dataAdapter.Fill(resultDataSet);
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.ToString());
            }
            
            return resultDataSet;
        }
    }
}

