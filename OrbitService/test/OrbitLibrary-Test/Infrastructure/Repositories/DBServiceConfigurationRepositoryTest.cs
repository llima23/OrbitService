using System;
using Moq;
using System.Data;
using OrbitLibrary_Test.Builders;
using OrbitLibrary_Test.TestUtils;
using OrbitLibrary.Common;
using OrbitLibrary.Data;
using OrbitLibrary.Infrastructure.Repositories;
using Xunit;
using OrbitLibrary.Repositories;

namespace OrbitLibrary_Test.Infrastructure.Repositories
{
    public class DBServiceConfigurationRepositoryTest
    {
        public Mock<IWrapper> mWrapper;

        public DBServiceConfigurationRepository cut;
        public ServiceConfiguration defaultConfiguration;

        public DBServiceConfigurationRepositoryTest()
        {
            mWrapper = new Mock<IWrapper>();
            defaultConfiguration = TestsBuilder.CreateFakeConfigurationForTest();
            cut = new DBServiceConfigurationRepository(mWrapper.Object);
        }

        [Fact]
        public void ShouldReturnsAServiceConfigurationObject()
        {
            //Controlled result expected
            DataSet dsResult = new DataSet();
            DataTable table = new DataTable(DBServiceConfigurationRepository.TABLE_NAME);

            DataColumn column = new DataColumn();
            column.ColumnName = "U_TAX4_UrlOrbit";
            table.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "U_TAX4_usr4TAX";
            table.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "U_TAX4_pass4TAX";
            table.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "U_TAX4_TenantId";
            table.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "U_TAX4_EnvmentType";
            table.Columns.Add(column);

            dsResult.Tables.Add(table);

            DataRow config = dsResult.Tables[0].NewRow();
            config["U_TAX4_UrlOrbit"] = defaultConfiguration.GetBaseURI();
            config["U_TAX4_usr4TAX"]  = defaultConfiguration.Username;
            config["U_TAX4_pass4TAX"] = defaultConfiguration.Password;
            config["U_TAX4_TenantId"] = defaultConfiguration.TenantID.ToString();

            dsResult.Tables[DBServiceConfigurationRepository.TABLE_NAME].Rows.Add(config);

            //Returns the result expected
            string commandQueryExpected = @$"SELECT * FROM ""@{DBServiceConfigurationRepository.TABLE_NAME}""";
            mWrapper
                .Setup(m => m.ExecuteQuery(commandQueryExpected))
                .Returns(dsResult);


            //Controlled result expected
            dsResult = new DataSet();
            table = new DataTable(DBServiceConfigurationRepository.TABLE_NAME);

            column = new DataColumn();
            column.ColumnName = "TableName";
            table.Columns.Add(column);

            dsResult.Tables.Add(table);

            config = dsResult.Tables[0].NewRow();
            config["TableName"] = DBServiceConfigurationRepository.TABLE_NAME;

            dsResult.Tables[DBServiceConfigurationRepository.TABLE_NAME].Rows.Add(config);

            //Returns the result expected
            commandQueryExpected = @$"select * from OUTB where ""TableName"" = '{DBServiceConfigurationRepository.TABLE_NAME}'";
            mWrapper
              .Setup(m => m.ExecuteQuery(commandQueryExpected))
              .Returns(dsResult);


            ServiceConfiguration sConfig = cut.GetConfiguration();

            //Check execution result and returns
            Assert.NotNull(sConfig);
            Assert.Equal(defaultConfiguration.GetBaseURI(), sConfig.GetBaseURI());
            Assert.Equal(defaultConfiguration.TenantID.ToString(), sConfig.TenantID.ToString());
            Assert.Equal(defaultConfiguration.Username, sConfig.Username);
            Assert.Equal(defaultConfiguration.Password, sConfig.Password);
        }

        [Fact]
        public void ShouldVerifyIfTableConfigAddonExistTrue()
        {

            //Controlled result expected
            DataSet dsResult = new DataSet();
            DataTable table = new DataTable(DBServiceConfigurationRepository.TABLE_NAME);

            DataColumn column = new DataColumn();
            column.ColumnName = "TableName";
            table.Columns.Add(column);

            dsResult.Tables.Add(table);

            DataRow config = dsResult.Tables[0].NewRow();
            config["TableName"] = DBServiceConfigurationRepository.TABLE_NAME;

            dsResult.Tables[DBServiceConfigurationRepository.TABLE_NAME].Rows.Add(config);

            //Returns the result expected
            string commandQueryExpected = @$"select * from OUTB where ""TableName"" = '{DBServiceConfigurationRepository.TABLE_NAME}'";
            mWrapper
                .Setup(m => m.ExecuteQuery(commandQueryExpected))
                .Returns(dsResult);

            bool result = cut.VerifyIfTableConfigAddonExist();
            Assert.True(result);
        }

        [Fact]
        public void ShouldVerifyIfTableConfigAddonExistFalse()
        {

            //Controlled result expected
            DataSet dsResult = new DataSet();
            DataTable table = new DataTable(DBServiceConfigurationRepository.TABLE_NAME);

            DataColumn column = new DataColumn();
            column.ColumnName = "TableName";
            table.Columns.Add(column);

            dsResult.Tables.Add(table);

            //Returns the result expected
            string commandQueryExpected = @$"select * from OUTB where ""TableName"" = '{DBServiceConfigurationRepository.TABLE_NAME}'";
            mWrapper
                .Setup(m => m.ExecuteQuery(commandQueryExpected))
                .Returns(dsResult);

            bool result = cut.VerifyIfTableConfigAddonExist();
            Assert.False(result);
        }
    }
}

