using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.OutboundDFe.services
{
    public class OutboundDFeDocumentInutilServicesNFSe : BaseService<OutboundDFeDocumentInutilOutputNFSe, OutboundDFeDocumentInutilOutputNFSe>
    {
        public static string ENDPOINT = "/documentservice/api/nfse/inutilizacao";
        public OutboundDFeDocumentInutilServicesNFSe(ServiceConfiguration sConfig, CommunicationProvider communicationClient) : base(sConfig, communicationClient)
        {

        }

        public OperationResponse<OutboundDFeDocumentInutilOutputNFSe, OutboundDFeDocumentInutilOutputNFSe> Execute(OutboundDFeDocumentInutilInputNFSe input)
        {
            return InvokeOperation(
                 GetBuilder()
                     .EndpointPath(Method.PUT, ENDPOINT)
                     .CredentialsProvider(sConfig.CredentialsProvider)
                     .Body(input)
                     .Serializer(new JsonRequestBodySerializer(removeNullFields: true)),
                 new JsonResponseBodyDeserializer());
        }
    }
}
