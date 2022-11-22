using _4TAX_Service.Application.Handlers;
using _4TAX_Service.Common.Domain;
using _4TAX_Service.Infrastructure;
using _4TAX_Service.Services.Document.NFSe;
using Newtonsoft.Json;
using OrbitLibrary.Common;
using OrbitLibrary.Data;
using OrbitLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static _4TAX_Service.Services.Document.Properties.Emit;
using static _4TAX_Service.Services.Document.Properties.EmitResponse;

namespace _4TAX_Service.Application.Client
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

        public void IntegrateNFSe(List<NFSeB1Object> ListNFSe, EmitMapper emitMapper, Emit emit)
        {

            foreach (var item in ListNFSe)
            {
                try
                {
                    EmitRequestInput input = emitMapper.ConvertToOrbitObject(item);
                    var jsonInput = JsonConvert.SerializeObject(input, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    if(item.U_TAX4_tpOperacao == "Y")
                    {
                        Logs.InsertLog($"Json NFSe: {JsonConvert.SerializeObject(input)}");
                    }
                  
                    OperationResponse<EmitSuccessResponseOutput, EmitFailResponseOutput> response = SendNFSeToOrbit(item, emitMapper, emit);
                    Logs.InsertLog($"Response: {response.Content} + IsSucessfull: {response.isSuccessful}");
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

        public OperationResponse<EmitSuccessResponseOutput, EmitFailResponseOutput> SendNFSeToOrbit(dynamic item, EmitMapper emitMapper, Emit emit)
        {
            EmitRequestInput input = emitMapper.ConvertToOrbitObject(item);
            
            OperationResponse<EmitSuccessResponseOutput, EmitFailResponseOutput> response = emit.Execute(input, true);

            return response;
        }

        public bool UpdateStatusNFSeB1Sucess(OperationResponse<EmitSuccessResponseOutput, EmitFailResponseOutput> response, int DocEntry, int BPLId)
        {
            DataBaseNFSeProcess dataBaseNFSeProcess = new DataBaseNFSeProcess(dbWrapper);
            EmitSuccessResponseOutput output = response.GetSuccessResponse();
            Logs.InsertLog($"NFSe Inserida na fila de emissão: {DocEntry} nfsID: {output.nfseId}");
            //Logs.InsertLog($"Json NFSe: {JsonConvert.DeserializeObject(JsonConvert.SerializeObject(response.Request.Body))}");
            return dataBaseNFSeProcess.UpdateNFSeODBC(MyQuery.AtualizarNotaFiscalSuccess(DocEntry, BPLId, output.message, output.nfseId));
        }

        public bool UpdateStatusNFSeB1Failed(OperationResponse<EmitSuccessResponseOutput, EmitFailResponseOutput> response, int DocEntry, int BPLId)
        {
            DataBaseNFSeProcess dataBaseNFSeProcess = new DataBaseNFSeProcess(dbWrapper);
            EmitFailResponseOutput output = response.GetErrorResponse();            
            string msgError = $"{output.errors.Select(x => x.msg).FirstOrDefault()} - {output.errors.Select(x => x.param).FirstOrDefault()}";            
            Logs.InsertLog($"Erro ao enviar NFSe para Orbit: {DocEntry} Erro: {msgError}");
            return dataBaseNFSeProcess.UpdateNFSeODBC(MyQuery.AtualizarNotaFiscalFail(DocEntry, BPLId, msgError));
        }
    }
}
