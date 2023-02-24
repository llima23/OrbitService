using B1Library.Documents;
using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService_NFSe.New_Atualiza_NFSe.OutboundDFe.services
{
    public class AtualizaNFSeService : BaseService<AtualizaNFSeOutput, AtualizaNFSeError>
    {
        public static string ENDPOINT = "/documentservice/api/nfse/consulta/{id}";
        public AtualizaNFSeService(ServiceConfiguration sConfig, CommunicationProvider communicationClient) : base(sConfig, communicationClient)
        {

        }

        public OperationResponse<AtualizaNFSeOutput, AtualizaNFSeError> Execute(Invoice invoice)
        {
            return InvokeOperation(
                 GetBuilder()
                     .EndpointPath(Method.GET, ENDPOINT)
                     .CredentialsProvider(sConfig.CredentialsProvider)
                     .SetPathParam("{id}", invoice.IdRetornoOrbit)
                     .Serializer(new JsonRequestBodySerializer(removeNullFields: true)),
                 new JsonResponseBodyDeserializer());
        }
    }
}
