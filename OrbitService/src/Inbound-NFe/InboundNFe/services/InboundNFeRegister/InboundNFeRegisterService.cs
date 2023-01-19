using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.InboundNFe.services.InboundNFeRegister
{
    public class InboundNFeRegisterService : BaseService<InboundNFeDocumentRegisterOutput, InboundNFeDocumentRegisterError>
    {
        //TODO: Verificar ENDPOINT
        public const string ENDPOINT = "/taxdocumentservice/api/nfe";

        public InboundNFeRegisterService(ServiceConfiguration sConfig, CommunicationProvider communicationClient) : base(sConfig, communicationClient)
        {

        }

        public OperationResponse<InboundNFeDocumentRegisterOutput, InboundNFeDocumentRegisterError> Execute(Root input)
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
