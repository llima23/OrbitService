using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.InboundCce.services
{
    public class InboundCceService : BaseService<InboundCceOutput, InboundCceError>
    {
        //TODO: Verificar ENDPOINT
        public const string ENDPOINT = "/taxdocumentservice/api/dfe";

        public InboundCceService(ServiceConfiguration sConfig, CommunicationProvider communicationClient) : base(sConfig, communicationClient)
        {

        }

        public OperationResponse<InboundCceOutput, InboundCceError> Execute(InboundCceInput input)
        {
            return InvokeOperation(
                    GetBuilder()
                        .EndpointPath(Method.POST, ENDPOINT)
                        .CredentialsProvider(sConfig.CredentialsProvider)
                        .AddHeader("target", "taxdocumentservice")
                        .Body(input)
                        .Serializer(new JsonRequestBodySerializer(removeNullFields: true)),
                    new JsonResponseBodyDeserializer());
        }
    }
}
