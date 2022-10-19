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
            tableName.ObjB1Type = 13;
            tableName.TableChild = tableName.TableHeader.Remove(0, 1);
            tableName.Type = Type.Saida;
            tables.Add(tableName);

            tableName = new TableName();
            tableName.TableHeader = "ODLN";
            tableName.Type = Type.Saida;
            tableName.ObjB1Type = 15;
            tableName.TableChild = tableName.TableHeader.Remove(0, 1);
            tables.Add(tableName);

            tableName = new TableName();
            tableName.TableHeader = "OPCH";
            tableName.Type = Type.Entrada;
            tableName.ObjB1Type = 18;
            tableName.TableChild = tableName.TableHeader.Remove(0, 1);
            tables.Add(tableName);

            tableName = new TableName();
            tableName.TableHeader = "ORPD";
            tableName.Type = Type.Entrada;
            tableName.ObjB1Type = 21;
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
            tableName.ObjB1Type = 18;
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
            tableName.ObjB1Type = 13;
            tableName.TableChild = tableName.TableHeader.Remove(0, 1);
            tableName.Type = Type.Saida;
            tables.Add(tableName);

            tableName = new TableName();
            tableName.TableHeader = "ORPC";
            tableName.Type = Type.Saida;
            tableName.ObjB1Type = 19;
            tableName.TableChild = tableName.TableHeader.Remove(0, 1);
            tables.Add(tableName);

            tableName = new TableName();
            tableName.TableHeader = "ODLN";
            tableName.Type = Type.Saida;
            tableName.ObjB1Type = 15;
            tableName.TableChild = tableName.TableHeader.Remove(0, 1);
            tables.Add(tableName);

            tableName = new TableName();
            tableName.TableHeader = "ORPD";
            tableName.Type = Type.Saida;
            tableName.ObjB1Type = 21;
            tableName.TableChild = tableName.TableHeader.Remove(0, 1);
            tables.Add(tableName);

            tableName = new TableName();
            tableName.TableHeader = "OPCH";
            tableName.ObjB1Type = 18;
            tableName.TableChild = tableName.TableHeader.Remove(0, 1);
            tableName.Type = Type.Entrada;
            tables.Add(tableName);

            tableName = new TableName();
            tableName.TableHeader = "OPDN";
            tableName.ObjB1Type = 20;
            tableName.Type = Type.Entrada;
            tableName.TableChild = tableName.TableHeader.Remove(0, 1);
            tables.Add(tableName);

            tableName = new TableName();
            tableName.TableHeader = "ORIN";
            tableName.Type = Type.Entrada;
            tableName.ObjB1Type = 14;
            tableName.TableChild = tableName.TableHeader.Remove(0, 1);
            tables.Add(tableName);

            tableName = new TableName();
            tableName.TableHeader = "ORDN";
            tableName.Type = Type.Entrada;
            tableName.ObjB1Type = 16;
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
            tableName.ObjB1Type = 13;
            tableName.Type = Type.Saida;
            tableName.TableChild = tableName.TableHeader.Remove(0, 1);
            tables.Add(tableName);
            return tables;
        }

        public class TableName
        {
            public string TableHeader { get; set; }
            public string TableChild { get; set; }
            public int ObjB1Type { get; set; }
            public Type Type { get; set; }
        }

        public enum Type
        {
            Saida = 0,
            Entrada = 1
        }
    }
}
