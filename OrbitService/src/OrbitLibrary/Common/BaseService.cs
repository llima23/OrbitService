using OrbitLibrary.Common.Interfaces;

namespace OrbitLibrary.Common
{
    public abstract class BaseService<TSuccess,TError>
    {
        private const int RETRY_MAX_ATTEMPTS = 1;
        protected ServiceConfiguration sConfig { get; }
        protected CommunicationProvider Client { get; }

        public BaseService(ServiceConfiguration sConfig, CommunicationProvider communicationClient) {
            this.sConfig = sConfig;
            Client = communicationClient;
        }

        protected OperationResponse<TSuccess,TError> InvokeOperation(OperationRequestBuilder builder, ResponseBodyDeserializerHandler handle)
        {
            int retryTimes = 0;
            OperationResponse<TSuccess, TError> response = Client.Send<TSuccess,TError>(builder.Build());

            if (builder.ShouldPutAuthentication())
            {
                while(response.StatusCode == System.Net.HttpStatusCode.Unauthorized && retryTimes < RETRY_MAX_ATTEMPTS)
                {
                    response = Client.Send<TSuccess, TError>(builder.ForceNewAuthentication().Build());
                    retryTimes++;
                }
            }
            response.DeserializerHandle = handle;
            return response;
        }

        virtual protected OperationRequestBuilder GetBuilder()
        {
            return new OperationRequestBuilder(sConfig);
        }
    }
}
