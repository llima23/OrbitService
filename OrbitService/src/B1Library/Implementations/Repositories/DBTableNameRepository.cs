using System;
using System.Collections.Generic;
using System.Text;

namespace B1Library.Implementations.Repositories
{
    public class DBTableNameRepository
    {

        public List<TableName> tableNames { get { return GetTableNames(); } }

        public List<TableName> GetTableNames()
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
