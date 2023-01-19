using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.OutboundDFe.services
{
    public class OutboundDFeDocumentInutilServicesNFe : BaseService<OutboundDFeDocumentInutilOutputNFe, OutboundDFeDocumentInutilOutputNFe>
    {
        public static string ENDPOINT = "/documentservice/api/nfe/inutilizacao";
        public OutboundDFeDocumentInutilServicesNFe(ServiceConfiguration sConfig, CommunicationProvider communicationClient) : base(sConfig, communicationClient)
        {

        }
        public OperationResponse<OutboundDFeDocumentInutilOutputNFe, OutboundDFeDocumentInutilOutputNFe> Execute(OutboundDFeDocumentInutilInputNFe input)
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
