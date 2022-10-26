using OrbitLibrary.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace _4TAX_Service_Atualiza.Application.Client
{
    class DataBaseNFSeProcess
    {
        private IWrapper dbWrapper;

        public DataBaseNFSeProcess(IWrapper wrapper)
        {
            dbWrapper = wrapper;
        }

        public bool UpdateNFSeODBC(string sQuery)
        {
            int dsResult = dbWrapper.ExecuteNonQuery(sQuery);  //TODO Resources
            
            if (dsResult > 0)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }
    }
}
