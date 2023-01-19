using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.OutboundDFe.services
{
    public class OutboundDFeDocumentCancelServiceNFe : BaseService<OutboundDFeDocumentCancelOutputNFe, OutboundDFeDocumentCancelOutputNFe>
    {
        public static string ENDPOINT = "/documentservice/api/nfe/cancel";

        public OutboundDFeDocumentCancelServiceNFe(ServiceConfiguration sConfig, CommunicationProvider communicationClient) : base(sConfig, communicationClient)
        {

        }
        public OperationResponse<OutboundDFeDocumentCancelOutputNFe, OutboundDFeDocumentCancelOutputNFe> Execute(OutboundDFeDocumentCancelInputNFe input)
        {
            return InvokeOperation(
                 GetBuilder()
                     .EndpointPath(Method.POST, ENDPOINT)
                     .CredentialsProvider(sConfig.CredentialsProvider)
                     .Body(input)
                     .Serializer(new JsonRequestBodySerializer(removeNullFields: true)),
                 new JsonResponseBodyDeserializer());
        }

    }
}
