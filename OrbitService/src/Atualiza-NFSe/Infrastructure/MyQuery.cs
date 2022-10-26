using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;
using static _4TAX_Service_Atualiza.Services.Document.Properties.Consulta.ConsultaSuccessResponseOutput;

namespace _4TAX_Service_Atualiza.Infrastructure
{

    public class ParameterForQuery
    {
        public int IdDocumento { get; set; }
        public int IdFilialSAP { get; set; }
        public string RetornoOrbit { get; set; }
        public string IdRetornoOrbit { get; set; }

    }
    public static class MyQuery
    {
        public static string QueryUpdateStatusSuccessInB1(int DocEntry, int? BplId, string mStat, string idOrbit, string CodVeri, string NumeroNfse, string NumeroRPS)
        {           
            return $@"UPDATE OINV 
                         SET ""U_TAX4_Stat"" = '{mStat}', 
                             ""U_TAX4_CodInt"" = '{GetStatusOrbitToB1(mStat)}',
                             ""U_TAX4_IdRet"" = '{idOrbit}',
                             ""U_TAX4_CodVeri"" = '{CodVeri}',
                             ""U_TAX4_NumeroNfse"" = '{NumeroNfse}',
                             ""U_TAX4_NumeroRPS"" = '{NumeroRPS}'
                       WHERE ""DocEntry"" = {DocEntry} 
                         {(BplId == null ? "" : $@"AND ""BPLId"" = {BplId}")}";
        }
        public static string QueryUpdateStatusFailInB1(int docEntry, int? BPLId, string statRet)
        {
            return $@"UPDATE OINV 
                         SET ""U_TAX4_Stat"" = '{statRet}', 
                             ""U_TAX4_CodInt"" = '3'                             
                       WHERE ""DocEntry"" = {docEntry} 
                         {(BPLId == null ? "" : $@"AND ""BPLId"" = {BPLId}")}";
        }


        public static string GetStatusOrbitToB1(string statusOrbit)
        {
            string StatusB1 = string.Empty;

            switch (statusOrbit)
            {
                case StatusMessageOrbitList.EmProcesso:
                    StatusB1 = "1";
                    break;
                case StatusMessageOrbitList.EnvioEmProcesso:
                    StatusB1 = "1";
                    break;
                case StatusMessageOrbitList.RPSHomologado:
                    StatusB1 = "2";
                    break;
                case StatusMessageOrbitList.RPSEmitida:
                    StatusB1 = "2";
                    break;
                case StatusMessageOrbitList.NFSeEmitida:
                    StatusB1 = "2";
                    break;
                case StatusMessageOrbitList.RpsNaoEmitida:
                    StatusB1 = "3";
                    break;
                case StatusMessageOrbitList.UnknowError:
                    StatusB1 = "3";
                    break;
                case StatusMessageOrbitList.ValidationError:
                    StatusB1 = "3";
                    break;
                case StatusMessageOrbitList.UnwantedTwin:
                    StatusB1 = "3";
                    break;
                case StatusMessageOrbitList.CancelamentoEmProcesso:
                    StatusB1 = "4";
                    break;
                case StatusMessageOrbitList.CancelamentoEmProcessoPrefeitura:
                    StatusB1 = "4";
                    break;
                case StatusMessageOrbitList.Cancelada:
                    StatusB1 = "5";
                    break;
                case StatusMessageOrbitList.Inutilizada:
                    StatusB1 = "6";
                    break;
                default:                    
                    StatusB1 = "3";
                    break;
            }

            return StatusB1;
        }
    }
}

