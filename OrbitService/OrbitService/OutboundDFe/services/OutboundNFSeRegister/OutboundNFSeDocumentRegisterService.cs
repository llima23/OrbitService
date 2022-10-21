using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitService.OutboundDFe.services.OutboundNFSeRegister
{
    public class OutboundNFSeDocumentRegisterService : BaseService<OutboundNFSeDocumentRegisterOutput, OutboundNFSeDocumentRegisterError>
    {
        public const string ENDPOINT = "/documentservice/api/nfse/requestEmit";
        public OutboundNFSeDocumentRegisterService(ServiceConfiguration sConfig, CommunicationProvider communicationClient) : base(sConfig, communicationClient)
        {

        }

        public OperationResponse<OutboundNFSeDocumentRegisterOutput, OutboundNFSeDocumentRegisterError> Execute(OutboundNFSeDocumentRegisterInput input)
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
