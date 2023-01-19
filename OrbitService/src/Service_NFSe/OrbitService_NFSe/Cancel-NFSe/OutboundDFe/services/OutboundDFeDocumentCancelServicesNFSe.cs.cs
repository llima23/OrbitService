using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService_Cancel_NFSe.OutboundDFe.services
{
    public class OutboundDFeDocumentCancelServicesNFSe : BaseService<OutboundDFeDocumentCancelOutputNFSe, OutboundDFeDocumentCancelOutputNFSe>
    {
        public static string ENDPOINT = "/documentservice/api/nfse/cancel";

        public OutboundDFeDocumentCancelServicesNFSe(ServiceConfiguration sConfig, CommunicationProvider communicationClient) : base(sConfig, communicationClient)
        {

        }

        public OperationResponse<OutboundDFeDocumentCancelOutputNFSe, OutboundDFeDocumentCancelOutputNFSe> Execute(OutboundDFeDocumentCancelInputNFSe input)
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
