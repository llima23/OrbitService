using _4TAX_Service.Common.Domain;
using _4TAX_Service.Common.Handlers;
using _4TAX_Service.Services.Document.NFSe;
using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;
using static _4TAX_Service.Services.Document.Properties.Emit;
using static _4TAX_Service.Services.Document.Properties.EmitResponse;

namespace _4TAX_Service.Application.Handlers
{
    interface NFSeHandler 
    {
        void IntegrateNFSe(List<NFSeB1Object> ListNFSe, EmitMapper emitMapper, Emit emit);
        OperationResponse<EmitSuccessResponseOutput, EmitFailResponseOutput> SendNFSeToOrbit(dynamic item, EmitMapper emitMapper, Emit emit);
        bool UpdateStatusNFSeB1Sucess(OperationResponse<EmitSuccessResponseOutput, EmitFailResponseOutput> response, int DocEntry, int BPLId);
        bool UpdateStatusNFSeB1Failed(OperationResponse<EmitSuccessResponseOutput, EmitFailResponseOutput> response, int DocEntry, int BPLId);
    }
}
