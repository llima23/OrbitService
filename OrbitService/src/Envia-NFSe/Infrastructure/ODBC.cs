using _4TAX_Service.Application;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Text;


namespace _4TAX_Service.Infrastructure
{
    public class ODBC
    {
        private OdbcConnection connODBC = null;
        private OdbcCommand cmdODBC = null;
        private OdbcDataAdapter daODBC = null;
        private DataSet ds = null;        

        /// <summary>
        /// Propriedades DatabaseConnectionProp
        /// </summary>
        public static class DatabaseConnectionProp
        {
            public static OdbcConnectionStringBuilder HanaODBC { get; set; }

            public static string HanaConnectionString { get; set; }
            public static string Server { get; set; }
            public static string User { get; set; }
            public static string Pass { get; set; }
            public static string Base { get; set; }
        }

        /// <summary>
        /// Abrir Conexão DB HANA com ODBC
        /// </summary>
        /// <returns></returns>
        public OdbcConnection OpenConnODBC()
        {
            try
            {
                string HanaConnectionString = PreencherConnectionStringODBC();
                connODBC = new OdbcConnection(HanaConnectionString);
                connODBC.Open();
                return connODBC;
            }
            catch (Exception ex)
            {
                Logs.InsertLog($"Erro ao abrir conexão com o Banco de Dados: {ex}");
                throw;
            }
        }

        /// <summary>
        /// Fechar Conexão ODBC HANA
        /// </summary>
        /// <param name="connODBC"></param>
        public void CloseConnODBC(OdbcConnection connODBC)
        {
            if (connODBC.State == System.Data.ConnectionState.Open)
            {
                connODBC.Close();
            }
        }

        /// <summary>
        /// Montar connection String com a deserialização do objeto de conexão 
        /// </summary>
        /// <param name="databaseConnection">DatabaseConnectionProp</param>
        /// <returns>DRIVER={HDBODBC};UID=SYSTEM;PWD=senhaBanco;SERVERNODE=hanadb:30015;CS=SBO_OUTBOUNDDEV;</returns>
        public static string PreencherConnectionStringODBC()
        {
            try
            {
                MontaObjetoConexaoODBC();
                StringBuilder sbConnectionString = new StringBuilder();
                sbConnectionString.Append("DRIVER={HDBODBC};");
                sbConnectionString.Append($"UID={DatabaseConnectionProp.User};");
                sbConnectionString.Append($"PWD={DatabaseConnectionProp.Pass};");
                sbConnectionString.Append($"SERVERNODE={DatabaseConnectionProp.Server};");
                sbConnectionString.Append($"CS={DatabaseConnectionProp.Base};");
                DatabaseConnectionProp.HanaConnectionString = sbConnectionString.ToString();
                return DatabaseConnectionProp.HanaConnectionString;
            }
            catch (Exception ex)
            {
                Logs.InsertLog($"Erro ao montar ConnectionString : {ex}");
                throw;
            }
        }

        /// <summary>
        /// Montar Objeto de conexão ODBC
        /// </summary>
        /// <returns>DatabaseConnectionProp</returns>
        public static void MontaObjetoConexaoODBC()
        {
            try
            {
                var AppSettings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();                
                DatabaseConnectionProp.Server = Crypto.Decrypt(AppSettings.GetSection("HanaConnection")["Server"]);
                DatabaseConnectionProp.User = Crypto.Decrypt(AppSettings.GetSection("HanaConnection")["User"]);
                DatabaseConnectionProp.Pass = Crypto.Decrypt(AppSettings.GetSection("HanaConnection")["Pass"]);
                DatabaseConnectionProp.Base = Crypto.Decrypt(AppSettings.GetSection("HanaConnection")["Base"]);                
            }
            catch (Exception ex)
            {
                Logs.InsertLog($"Erro ao montar Objeto de conexão : {ex}");
                throw;
            }
        }

        /// <summary>
        /// Retorna DataSet através de uma insrução SQL conexão ODBC
        /// </summary>
        /// <param name="sQuery">SELECT * FROM OINV</param>
        /// <returns></returns>
        public DataSet RetornaDataSetODBC(string sQuery, string sWhere = null)
        {
            try
            {
                connODBC = new OdbcConnection();
                connODBC = OpenConnODBC();
                cmdODBC = new OdbcCommand();
                if (!sQuery.Contains("SELECT"))
                {
                    sQuery = $"SELECT * FROM \"{sQuery}\" ";

                    if (!String.IsNullOrEmpty(sWhere))
                    {
                        sQuery += $" WHERE {sWhere}";
                    }                    
                }
                cmdODBC.CommandText = sQuery;
                cmdODBC.Connection = connODBC;
                daODBC = new OdbcDataAdapter();
                ds = new DataSet();
                daODBC.SelectCommand = cmdODBC;
                daODBC.SelectCommand.CommandTimeout = 600;
                daODBC.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                Logs.InsertLog($"Erro ao executar Query no Banco de dados: {ex}");
                return null;
            }
            finally
            {
                CloseConnODBC(connODBC);
            }
        }

        public bool AtualizaDadosODBC(string sQueryUpdate)
        {
            bool atualizado = false;
            try
            {
                connODBC = new OdbcConnection();
                connODBC = OpenConnODBC();
                cmdODBC = new OdbcCommand();
                cmdODBC.CommandText = sQueryUpdate;
                cmdODBC.Connection = connODBC;
                int recordsAffected = cmdODBC.ExecuteNonQuery();
                if (recordsAffected > 0) atualizado = true;

                return atualizado;
            }
            catch (Exception ex)
            {
                Logs.InsertLog($"Erro ao executar Query no Banco de dados: {ex}");
                return atualizado;
            }
            finally
            {
                CloseConnODBC(connODBC);
            }
        }

    }
}
