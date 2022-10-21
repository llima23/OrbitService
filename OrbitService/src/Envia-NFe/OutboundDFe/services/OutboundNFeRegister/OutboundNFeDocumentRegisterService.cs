using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;
using static OrbitService.OutboundDFe.services.OutboundDFeRegister.OutboundNFeDocumentRegisterInput;

namespace OrbitService.OutboundDFe.services.OutboundDFeRegister
{
    public class OutboundNFeDocumentRegisterService : BaseService<OutboundNFeDocumentRegisterOutput, OutboundNFeDocumentRegisterError>
    {
        public const string ENDPOINT = "/documentservice/api/nfe/requestEmit";
        public OutboundNFeDocumentRegisterService(ServiceConfiguration sConfig, CommunicationProvider communicationClient) : base(sConfig, communicationClient)
        {

        }

        public OperationResponse<OutboundNFeDocumentRegisterOutput, OutboundNFeDocumentRegisterError> Execute(OutboundNFeDocumentRegisterInput input)
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
