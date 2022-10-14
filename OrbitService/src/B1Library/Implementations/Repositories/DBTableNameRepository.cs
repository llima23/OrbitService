using System;
using System.Collections.Generic;
using System.Text;

namespace B1Library.Implementations.Repositories
{
    public class DBTableNameRepository
    {

        public List<TableName> tableNamesOtherDocuments { get { return GetTableNamesOtherDocuments(); } }

        public List<TableName> GetTableNamesOtherDocuments()
        {
            List<TableName> tables = new List<TableName>();
            TableName tableName = new TableName();
            tableName.TableHeader = "OINV";
            tableName.TableChild = tableName.TableHeader.Remove(0, 1);
            tableName.Type = Type.Saida;
            tables.Add(tableName);

            tableName = new TableName();
            tableName.TableHeader = "ODLN";
            tableName.Type = Type.Saida;
            tableName.TableChild = tableName.TableHeader.Remove(0, 1);
            tables.Add(tableName);

            tableName = new TableName();
            tableName.TableHeader = "OPCH";
            tableName.Type = Type.Entrada;
            tableName.TableChild = tableName.TableHeader.Remove(0, 1);
            tables.Add(tableName);

            tableName = new TableName();
            tableName.TableHeader = "ORPD";
            tableName.Type = Type.Entrada;
            tableName.TableChild = tableName.TableHeader.Remove(0, 1);
            tables.Add(tableName);

            return tables;
        }

        public List<TableName> tableNamesInbound { get { return GetTableNamesInbound(); } }

        public List<TableName> GetTableNamesInbound()
        {
            List<TableName> tables = new List<TableName>();
            TableName tableName = new TableName();
            tableName.TableHeader = "OPCH";
            tableName.Type = Type.Entrada;
            tableName.TableChild = tableName.TableHeader.Remove(0, 1);
            tables.Add(tableName);
            return tables;
        }


        public List<TableName> tableNamesOutboundNFe { get { return GetTableNamesOutboundNFe(); } }

        public List<TableName> GetTableNamesOutboundNFe()
        {
            List<TableName> tables = new List<TableName>();
            TableName tableName = new TableName();
            tableName.TableHeader = "OINV";
            tableName.TableChild = tableName.TableHeader.Remove(0, 1);
            tableName.Type = Type.Saida;
            tables.Add(tableName);

            tableName = new TableName();
            tableName.TableHeader = "ORPC";
            tableName.Type = Type.Saida;
            tableName.TableChild = tableName.TableHeader.Remove(0, 1);
            tables.Add(tableName);

            tableName = new TableName();
            tableName.TableHeader = "ODLN";
            tableName.Type = Type.Saida;
            tableName.TableChild = tableName.TableHeader.Remove(0, 1);
            tables.Add(tableName);

            tableName = new TableName();
            tableName.TableHeader = "ORPD";
            tableName.Type = Type.Saida;
            tableName.TableChild = tableName.TableHeader.Remove(0, 1);
            tables.Add(tableName);
      
            tableName.TableHeader = "OPCH";
            tableName.TableChild = tableName.TableHeader.Remove(0, 1);
            tableName.Type = Type.Entrada;
            tables.Add(tableName);

            tableName = new TableName();
            tableName.TableHeader = "OPDN";
            tableName.Type = Type.Entrada;
            tableName.TableChild = tableName.TableHeader.Remove(0, 1);
            tables.Add(tableName);

            tableName = new TableName();
            tableName.TableHeader = "ORIN";
            tableName.Type = Type.Entrada;
            tableName.TableChild = tableName.TableHeader.Remove(0, 1);
            tables.Add(tableName);

            tableName = new TableName();
            tableName.TableHeader = "ORDN";
            tableName.Type = Type.Entrada;
            tableName.TableChild = tableName.TableHeader.Remove(0, 1);
            tables.Add(tableName);

            return tables;
        }

        public List<TableName> tableNamesOutboundNFSe { get { return GetTableNamesOutboundNFSe(); } }

        public List<TableName> GetTableNamesOutboundNFSe()
        {
            List<TableName> tables = new List<TableName>();
            TableName tableName = new TableName();
            tableName.TableHeader = "OINV";
            tableName.Type = Type.Saida;
            tableName.TableChild = tableName.TableHeader.Remove(0, 1);
            tables.Add(tableName);
            return tables;
        }

        public class TableName
        {
            public string TableHeader { get; set; }
            public string TableChild { get; set; }
            public Type Type { get; set; }
        }

        public enum Type
        {
            Saida = 0,
            Entrada = 1
        }
    }
}
