using OrbitService.InboundOtherDocuments.services;
using OrbitService.InboundOtherDocuments.services.Input;
using Xunit;

namespace OrbitService_Test.FiscalBrazil.services
{
    public class FactoryTest
    {
        [Fact]
        public void ShouldCreateOtherDocumentRegisterInput()
        {
            OtherDocumentRegisterInput input = Factory.CreateOtherDocumentRegisterInputInstance();

            Assert.NotNull(input);
            Assert.NotNull(input.Identificacao);
            Assert.NotNull(input.Emitente);
            Assert.NotNull(input.Emitente.Endereco);
            Assert.NotNull(input.Destinatario);
            Assert.NotNull(input.Destinatario.Endereco);
            Assert.NotNull(input.Valores);
            Assert.NotNull(input.Itens);
            Assert.NotNull(input.Status);
        }
    }
}
