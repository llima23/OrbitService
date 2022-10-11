using OrbitLibrary.Infrastructure.Data;
using Xunit;
using System.Data;
using System.Data.Odbc;

namespace OrbitLibrary_Test.Infrastructure.Data
{
    public class ODBCDbFactoryTest
    {
        public ODBCDbFactory cut;
        const string validConnectionString = "DRIVER={HDBODBC};UID=user;PWD=password;SERVERNODE=server:3000;CS=db_name;";
        const string validDatabaseName = "db_name";
        const string validDatabaseType = "HANA";

        public ODBCDbFactoryTest()
        {
            cut = new ODBCDbFactory(validConnectionString)
            {
                DataBaseType = validDatabaseType,
                DataBaseName = validDatabaseName
            };
        }

        [Fact]
        public void ShouldReceiverDataBaseNameAndDataBaseType()
        {
            Assert.Equal(validDatabaseName, cut.DataBaseName);
            Assert.Equal(validDatabaseType, cut.DataBaseType);
        }

        [Fact]
        public void ShouldCreateAODBCConnectionObject()
        {
            IDbConnection connection = cut.CreateConnection();

            Assert.NotNull(connection);
            Assert.IsType<OdbcConnection>(connection);
            Assert.Equal(validConnectionString, connection.ConnectionString);
        }

        [Fact]
        public void ShouldCreateAODBCCommandObject()
        {
            IDbCommand command = cut.CreateCommand();

            Assert.NotNull(command);
            Assert.IsType<OdbcCommand>(command);
        }

        [Fact]
        public void ShouldCreateAODBCDataAdapterObject()
        {
            IDataAdapter dataAdapter = cut.CreateDataAdapter();

            Assert.NotNull(dataAdapter);
            Assert.IsType<OdbcDataAdapter>(dataAdapter);
        }
    }
}

