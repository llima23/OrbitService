using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectionStringGenerator.Application.ConnectionString
{
    public class ListConnectionString
    {
        public List<ObjConnectionString> connectionStrings { get; set; }
      
    }
    public class ObjConnectionString
    {
        public string ConnectionString { get; set; }
        public string DbServerType { get; set; }
        public string DataBaseName { get; set; }
    }
}
