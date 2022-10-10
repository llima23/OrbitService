using OrbitService.FiscalBrazil.services.NFSeDocumentRegister;
using Xunit;

namespace OrbitService_Test.FiscalBrazil.services
{
    public class FactoryTest
    {


        [Fact]
        public void ShouldCreateNFSeDocumentRegisterInputInstance()
        {
            NFSeDocumentRegisterInput input = FactoryNFSeDocumentRegisterInput.CreateNFSeDocumentRegisterInputInstance();

            Assert.NotNull(input);
            Assert.NotNull(input.NFServico);
            Assert.NotNull(input.NFServico.Rps.Prestador);
            Assert.NotNull(input.NFServico.Rps.Prestador.Endereco);
            Assert.NotNull(input.NFServico.Rps.Pag);
            Assert.NotNull(input.NFServico.Rps.Pag.DetPag);
            Assert.NotNull(input.NFServico.Rps.Identificacao);
            Assert.NotNull(input.NFServico.Rps.Servico);
            Assert.NotNull(input.NFServico.Rps.Servico.Valores);
            Assert.NotNull(input.NFServico.Rps.Servico.Valores.Cofins);
            Assert.NotNull(input.NFServico.Rps.Servico.Valores.Pis);
            Assert.NotNull(input.NFServico.Rps.Servico.Valores.Inss);
            Assert.NotNull(input.NFServico.Rps.Servico.Valores.Ir);
            Assert.NotNull(input.NFServico.Rps.Servico.Valores.Csll);
            Assert.NotNull(input.NFServico.Rps.Servico.Valores.Inss);
            Assert.NotNull(input.NFServico.Rps.Tomador);
            Assert.NotNull(input.NFServico.Rps.Tomador.Contato);
            Assert.NotNull(input.NFServico.Rps.Tomador.Endereco);
            Assert.NotNull(input.NFServico.Rps.Intermediario);
        }
    }
}
