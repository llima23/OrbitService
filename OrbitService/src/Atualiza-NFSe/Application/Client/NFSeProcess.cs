using _4TAX_Service_Atualiza.Application.Handlers;
using _4TAX_Service_Atualiza.Common.Domain;
using _4TAX_Service_Atualiza.Infrastructure;
using _4TAX_Service_Atualiza.Services.Document.NFSe;
using OrbitLibrary.Common;
using OrbitLibrary.Data;
using OrbitLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using static _4TAX_Service_Atualiza.Services.Document.Properties.Consulta;


namespace _4TAX_Service_Atualiza.Application.Client
{
    public class NFSeProcess : NFSeHandler
    {
        public ServiceConfiguration sConfig;
        public IWrapper dbWrapper;
        public NFSeProcess(ServiceConfiguration sConfig, IWrapper dbWrapper)
        {
            this.sConfig = sConfig;
            this.dbWrapper = dbWrapper;
        }

        public void IntegrateNFSe(List<NFSeB1Object> ListNFSe, Consulta consulta)
        {

            foreach (var item in ListNFSe)
            {
                try
                {
                    OperationResponse<ConsultaSuccessResponseOutput, ConsultaFailedResponseOutput> response = GetByIdNFSeOrbit(item.U_TAX4_IdRet, consulta);

                    if (response.isSuccessful)
                    {
                        UpdateStatusNFSeB1Sucess(response, item.DocEntry, item.BPLId);
                    }
                    else
                    {
                        UpdateStatusNFSeB1Failed(response, item.DocEntry, item.BPLId);
                    }
                }
                catch (Exception ex)
                {
                    Logs.InsertLog($"Erro so integrar NFSe: {item.DocEntry.ToString()} ERRO: {ex.Message}");
                }
                finally
                {
                    GC.Collect();
                }

            }
        }
        public OperationResponse<ConsultaSuccessResponseOutput, ConsultaFailedResponseOutput> GetByIdNFSeOrbit(dynamic item, Consulta consulta)
        {
            OperationResponse<ConsultaSuccessResponseOutput, ConsultaFailedResponseOutput> response = consulta.Execute(item, true);
            return response;
        }
        public bool UpdateStatusNFSeB1Sucess(OperationResponse<ConsultaSuccessResponseOutput, ConsultaFailedResponseOutput> response, int DocEntry, int BPLId)
        {
            DataBaseNFSeProcess dataBaseNFSeProcess = new DataBaseNFSeProcess(dbWrapper);
            ConsultaSuccessResponseOutput output = response.GetSuccessResponse();
            Type myType = typeof(ConsultaSuccessResponseOutput);
            // Get the PropertyInfo object by passing the property name.
            string recebeCodVeri = "";
            string recebeNumeroNFse = "";
            try
            {
                PropertyInfo CodVeri = myType.GetProperty(output.nfse.codigoVerificacao);
                PropertyInfo NumeroNfse = myType.GetProperty(output.nfse.numero);
                recebeCodVeri = output.nfse.codigoVerificacao;
                recebeNumeroNFse = output.nfse.numero;
            }
            catch
            {
                recebeCodVeri = string.Empty;
                recebeNumeroNFse = string.Empty;
            }
            Logs.InsertLog($"NFSe Integrada com sucesso: {DocEntry}  nfsID: {output._id}");           

            return dataBaseNFSeProcess.UpdateNFSeODBC(MyQuery.QueryUpdateStatusSuccessInB1(DocEntry, BPLId, output.status.mStat, output._id, recebeCodVeri, recebeNumeroNFse,output.rps.identificacao.numero));
        }
        public bool UpdateStatusNFSeB1Failed(OperationResponse<ConsultaSuccessResponseOutput, ConsultaFailedResponseOutput> response, int DocEntry, int BPLId)
        {
            DataBaseNFSeProcess dataBaseNFSeProcess = new DataBaseNFSeProcess(dbWrapper);
            ConsultaFailedResponseOutput output = response.GetErrorResponse();
            Logs.InsertLog($"NFSe integrada com erro: {DocEntry}  Output: {output}");
            return dataBaseNFSeProcess.UpdateNFSeODBC(MyQuery.QueryUpdateStatusFailInB1(DocEntry, BPLId, output.message));
        }
    }
}
