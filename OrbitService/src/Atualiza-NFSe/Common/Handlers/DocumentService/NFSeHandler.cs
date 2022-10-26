using _4TAX_Service_Atualiza.Common.Domain;
using _4TAX_Service_Atualiza.Common.Handlers;
using _4TAX_Service_Atualiza.Services.Document.NFSe;
using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;
using static _4TAX_Service_Atualiza.Services.Document.Properties.Consulta;

namespace _4TAX_Service_Atualiza.Application.Handlers
{
    interface NFSeHandler 
    {
        void IntegrateNFSe(List<NFSeB1Object> ListNFSe, Consulta consulta);
        OperationResponse<ConsultaSuccessResponseOutput, ConsultaFailedResponseOutput> GetByIdNFSeOrbit(dynamic item, Consulta consulta);
        bool UpdateStatusNFSeB1Sucess(OperationResponse<ConsultaSuccessResponseOutput, ConsultaFailedResponseOutput> response, int DocEntry, int BPLId);
        //bool UpdateStatusNFSeB1Failed(OperationResponse<ConsultaSuccessResponseOutput, ConsultaFailedResponseOutput> response, int DocEntry, int BPLId);
    }
}
