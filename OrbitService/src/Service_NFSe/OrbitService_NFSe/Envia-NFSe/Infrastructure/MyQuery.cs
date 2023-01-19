using System;
using System.Collections.Generic;
using System.Text;

namespace _4TAX_Service.Infrastructure
{
    public static class MyQuery
    {
        public static string AtualizarNotaFiscalSuccess(int docEntry, int BPLId, string statRet, string idRet)
        {
            string updateFields = $@"UPDATE OINV SET ""U_TAX4_Stat"" = '{statRet}', ""U_TAX4_LIDO"" = 'S', ""U_TAX4_IdRet"" = '{idRet}', ""U_TAX4_CodInt"" = '1'";
            string updateConditions = $@" WHERE ""DocEntry"" = {docEntry} AND ""BPLId"" = {BPLId}";
            string retUpdate = updateFields + updateConditions;

            return retUpdate;
        }

        public static string AtualizarNotaFiscalFail(int docEntry, int BPLId, string statRet)
        {
            string updateFields = $@"UPDATE OINV SET ""U_TAX4_Stat"" = '{statRet}', ""U_TAX4_LIDO"" = 'S', ""U_TAX4_CodInt"" = '3'";
            string updateConditions = $@" WHERE ""DocEntry"" = {docEntry} AND ""BPLId"" = {BPLId}";
            string retUpdate = updateFields + updateConditions;
          
            return retUpdate;
        }
    }
}

