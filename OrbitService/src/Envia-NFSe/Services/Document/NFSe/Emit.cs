using Newtonsoft.Json;
using OrbitLibrary.Common;
using OrbitLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using static _4TAX_Service.Services.Document.Properties.Emit;
using static _4TAX_Service.Services.Document.Properties.EmitResponse;

namespace _4TAX_Service.Services.Document.NFSe
{

    public partial class Emit : BaseService<EmitSuccessResponseOutput, EmitFailResponseOutput>
    {
        public const string ENDPOINT = "/documentservice/api/nfse/requestEmit";

        public Emit(ServiceConfiguration sConfig, CommunicationProvider client) : base(sConfig, client) { }

        public OperationResponse<EmitSuccessResponseOutput, EmitFailResponseOutput> Execute(EmitRequestInput emitRequestData, bool prod)
        {
            OperationRequestBuilder requestBuilder = GetBuilder()
                                                    .EndpointPath(Method.POST, ENDPOINT)
                                                    .CredentialsProvider(Defaults.GetCredentialsProvider(sConfig, Client))
                                                    .AddHeader("prod", prod.ToString())
                                                    .Body(emitRequestData)                                                    
                                                    .Serializer(new JsonRequestBodySerializer(removeNullFields: true));
            return InvokeOperation(requestBuilder, new JsonResponseBodyDeserializer());
        }
    }
}
