using System;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using Moq;
using OrbitLibrary.Data;
using Xunit;

namespace OrbitLibrary_Test.Data
{
    public class DbWrapperTest
    {
        public Mock<DBFactory> mFactory;
        public Mock<IDbConnection> mConnection;
        public Mock<IDbCommand> mCommand;
        public Mock<IDbDataAdapter> mDataAdapter;
        public DbWrapper cut;

        public DbWrapperTest()
        {
            mFactory = new Mock<DBFactory>();
            mConnection = new Mock<IDbConnection>();
            mCommand = new Mock<IDbCommand>();
            mDataAdapter = new Mock<IDbDataAdapter>();

            mFactory.Setup(m => m.CreateConnection()).Returns(mConnection.Object);
            mFactory.Setup(m => m.CreateCommand()).Returns(mCommand.Object);
            mFactory.Setup(m => m.CreateDataAdapter()).Returns(mDataAdapter.Object);
            mConnection.SetupAllProperties();
            mCommand.SetupAllProperties();
            mDataAdapter.SetupAllProperties();

            cut = new DbWrapper(mFactory.Object);
        }

        [Fact]
        public void ShouldExecuteQueryCommand()
        {
            DataSet dsResult = cut.ExecuteQueryCommand(mCommand.Object);

            //Method execution test
            mFactory.Verify(m => m.CreateConnection(), Times.Once());
            mFactory.Verify(m => m.CreateDataAdapter(), Times.Once());
            mConnection.Verify(m => m.Open(), Times.Once());
            mDataAdapter.Verify(m => m.Fill(dsResult), Times.Once());
            mConnection.Verify(m => m.Close(), Times.Once());

            //Method result
            Assert.NotNull(dsResult);
            //Check if the object reference is the same in both properties
            Assert.Same(mConnection.Object, mCommand.Object.Connection);
            Assert.Equal(DbWrapper.COMMAND_TIMEOUT, mCommand.Object.CommandTimeout);
            Assert.Same(mCommand.Object, mDataAdapter.Object.SelectCommand);
        }

        [Fact]
        public void ExecuteQueryCommand_ShouldThrowExceptionWhenTheCommandObjectIsNull()
        {
            Assert.Throws<ArgumentException>(() => cut.ExecuteQueryCommand(null));
        }

        [Fact]
        public void ShouldExecuteNonQueryCommand()
        {
            mCommand.Setup(m => m.ExecuteNonQuery()).Returns(1);

            int numberOfRowsAffected = cut.ExecuteNonQueryCommand(mCommand.Object);

            //Method execution test
            mFactory.Verify(m => m.CreateConnection(), Times.Once());
            mConnection.Verify(m => m.Open(), Times.Once());
            mCommand.Verify(m => m.ExecuteNonQuery(), Times.Once());
            mConnection.Verify(m => m.Close(), Times.Once());

            //Method result test
            Assert.Same(mConnection.Object, mCommand.Object.Connection);
            Assert.Equal(DbWrapper.COMMAND_TIMEOUT, mCommand.Object.CommandTimeout);
            Assert.Equal(1, numberOfRowsAffected);
        }

        [Fact]
        public void ExecuteNonQueryCommand_ShouldThrowExceptionWhenTheCommandObjectIsNull()
        {
            Assert.Throws<ArgumentException>(() => cut.ExecuteNonQueryCommand(null));
        }

        [Fact]
        public void ShouldExecuteAQueryByString()
        {
            const string selectCommand = "SELECT * FROM TABLE_TEST";
            DataSet result = cut.ExecuteQuery(selectCommand);

            mFactory.Verify(m => m.CreateCommand(), Times.Once());

            Assert.Equal(selectCommand, mCommand.Object.CommandText);
            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldExecuteNonQueryByString()
        {
            const string insertCommand = "INSERT INTO TABLE_TEST (column1) VALUES ('teste')";
            mCommand.Setup(m => m.ExecuteNonQuery()).Returns(1);

            int numberRowsAffected = cut.ExecuteNonQuery(insertCommand);

            mFactory.Verify(m => m.CreateCommand(), Times.Once());
            Assert.Equal(insertCommand, mCommand.Object.CommandText);
            Assert.Equal(1, numberRowsAffected);
        }

        [Fact]
        public void ShouldReturnsCanConnectDB()
        {

            Assert.True(cut.CanDbConnect());
            mFactory.Verify(m => m.CreateConnection(), Times.Once());
            mConnection.Verify(m => m.Open(), Times.Once());
            mConnection.Verify(m => m.Close(), Times.Once());
        }

        [Fact]
        public void ShouldReturnsCannotConnectDB()
        {
            mConnection.Setup(m => m.Open()).Throws<ArgumentException>();

            Assert.False(cut.CanDbConnect());
            mFactory.Verify(m => m.CreateConnection(), Times.Once());
            mConnection.Verify(m => m.Open(), Times.Once());
        }
    }
}

