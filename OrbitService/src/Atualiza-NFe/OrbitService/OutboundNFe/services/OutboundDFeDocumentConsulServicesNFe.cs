using B1Library.Documents;
using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.OutboundNFe.services
{
    public class OutboundDFeDocumentConsulServicesNFe : BaseService<OutboundDFeDocumentConsultaOutputNFe, OutboundDFeDocumentConsulErroNFe>
    {
        public const string ENDPOINT = "/documentservice/api/nfe/consulta/{id}";
        public OutboundDFeDocumentConsulServicesNFe(ServiceConfiguration sConfig, CommunicationProvider communicationClient) : base(sConfig, communicationClient)
        {
        }
        public OperationResponse<OutboundDFeDocumentConsultaOutputNFe, OutboundDFeDocumentConsulErroNFe> Execute(Invoice invoice)
        {
            return InvokeOperation(
                 GetBuilder()
                     .EndpointPath(Method.GET, ENDPOINT)
                     .CredentialsProvider(sConfig.CredentialsProvider)
                     .SetPathParam("{id}",invoice.IdRetornoOrbit)
                     .Serializer(new JsonRequestBodySerializer(removeNullFields: true)),
                 new JsonResponseBodyDeserializer());
        }
    }
}
