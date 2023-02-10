using OrbitLibrary.Common;
using OrbitLibrary.Utils;
using OrbitService.InboundOtherDocuments.services.Error;
using OrbitService.InboundOtherDocuments.services.Input;
using OrbitService.InboundOtherDocuments.services.Output;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.InboundOtherDocuments.services
{
    public class OtherDocumentRegister : BaseService<OtherDocumentRegisterOutput, OtherDocumentRegisterError>
    {
        public const string ENDPOINT = "/taxdocumentservice/api/dfe";

        public OtherDocumentRegister(ServiceConfiguration sConfig, CommunicationProvider communicationClient) : base(sConfig, communicationClient)
        {

        }

        public OperationResponse<OtherDocumentRegisterOutput, OtherDocumentRegisterError> Execute(Root input)
        {
            return InvokeOperation(
                    GetBuilder()
                        .EndpointPath(Method.POST, ENDPOINT)
                        .AddHeader("target", "taxdocumentservice")
                        .CredentialsProvider(sConfig.CredentialsProvider)
                        .Body(input)
                        .Serializer(new JsonRequestBodySerializer(removeNullFields: true)),
                    new JsonResponseBodyDeserializer());
        }
    }
}
