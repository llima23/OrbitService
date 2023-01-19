using B1Library.Documents;
using OrbitService.InboundNFSe.mappers;
using OrbitService.InboundNFSe.services.NFSeDocumentRegister;
using OrbitService_Test.TestUtils;
using System;
using Xunit;

namespace OrbitService_Test.FiscalBrasil.mappers
{
    public class MapperNFSeDocumentTest
    {
        private MapperNFSeDocumentRegister cut;
        public MapperNFSeDocumentTest()
        {
            cut = new MapperNFSeDocumentRegister();
        }

        [Fact]
        public void ShouldConvertInvoiceB1ToOtherDocumentRegisterInput()
        {
            Invoice invoice = InvoiceB1FakeGenerator.GetFakeB1NFSeDocuments();
            NFSeDocumentRegisterInput input = new NFSeDocumentRegisterInput();
            input = cut.ToNFSeDocumentRegisterInput(invoice);

            Assert.NotNull(input);

            #region HEADER
            Assert.Equal(invoice.Identificacao.BranchId, input.NFServico.BranchId);
            Assert.Equal(invoice.Identificacao.DocEntry.ToString(), input.NFServico.numeroLote);
            Assert.Equal(invoice.Identificacao.DataLancamento.ToString("yyyy-MM-dd"), input.NFServico.dataLancamento);
            #endregion HEADER

            #region VALORES
            foreach (var Linhas in invoice.CabecalhoLinha)
            {
                Assert.Equal(input.NFServico.Rps.Servico.Quantidade, Convert.ToInt32(Linhas.QuantidadeLinha));
                Assert.Equal(input.NFServico.Rps.Servico.ValorUnitario, Linhas.ValorUnitarioLinha);
            }
            #endregion VALORES
        }

        [Fact]
        public void ShouldConvertOtherDocumentsRegisterOutputToInvoiceB1()
        {
            Invoice invoice = InvoiceB1FakeGenerator.GetFakeB1NFSeDocuments();
            NFSeDocumentRegisterOutput output = new NFSeDocumentRegisterOutput();
            //output.data.status = "sucess";
            DocumentStatus newStatus = cut.ToDocumentStatusResponseSucessful(invoice, output);

            Assert.NotNull(newStatus);
            Assert.Equal(invoice.DocEntry, newStatus.DocEntry);
            Assert.Equal(StatusCode.Sucess, newStatus.Status);
        }
    }
}
