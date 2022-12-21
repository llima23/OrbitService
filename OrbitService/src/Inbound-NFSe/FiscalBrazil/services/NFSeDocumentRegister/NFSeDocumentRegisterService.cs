using OrbitLibrary.Common;

namespace OrbitService.FiscalBrazil.services.NFSeDocumentRegister
{
    public class NFSeDocumentRegisterService : BaseService<NFSeDocumentRegisterOutput, NFSeDocumentRegisterError>
    {
        public const string ENDPOINT = "/taxdocumentservice/api/dfe";

        public NFSeDocumentRegisterService(ServiceConfiguration sConfig, CommunicationProvider communicationClient) : base(sConfig, communicationClient)
        {

        }

        public OperationResponse<NFSeDocumentRegisterOutput, NFSeDocumentRegisterError> Execute(NFSeDocumentRegisterInput input)
        {
            return InvokeOperation(
                    GetBuilder()
                        .EndpointPath(Method.POST, ENDPOINT)
                        .CredentialsProvider(sConfig.CredentialsProvider)
                        .AddHeader("target", "taxdocumentservice")
                        .Body(input)
                        .Serializer(new JsonRequestBodySerializer(removeNullFields: true)),
                    new JsonResponseBodyDeserializer());
        }
    }
}
