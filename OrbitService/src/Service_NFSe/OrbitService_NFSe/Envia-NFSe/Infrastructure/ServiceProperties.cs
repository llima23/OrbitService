using System;
using System.Collections.Generic;
using System.Text;

namespace _4TAX_Service.Infrastructure
{
    public class ServiceProperties
    {
        public enum DataSource
        {
            ODBC,
            DIAPI,          
            ServiceLayer
        }    
    }
}
