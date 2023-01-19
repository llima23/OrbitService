using B1Library.Documents;
using B1Library.Utilities;
using OrbitService.InboundOtherDocuments.mappers;
using OrbitService.InboundOtherDocuments.services.Input;
using OrbitService.InboundOtherDocuments.services.Output;
using OrbitService_Test.TestUtils;
using Xunit;

namespace OrbitService_Test.FiscalBrazil.mappers
{
    public class MapperTest
    {
        private Mapper cut;
        private Invoice invoice;
        private Util util;
        public MapperTest()
        {
            cut = new Mapper();
            invoice = InvoiceB1FakeGenerator.GetFakeInvoiceB1();
            util = new Util();
        }

        [Fact]
        public void ShouldConvertInvoiceB1ToOtherDocumentRegisterInput()
        {
            Invoice invoice = InvoiceB1FakeGenerator.GetFakeInvoiceB1();
            OtherDocumentRegisterInput input = new OtherDocumentRegisterInput();
            input = cut.ToOtherDocumentRegisterInput(invoice);

            Assert.NotNull(input);
            #region HEADER
            Assert.Equal(invoice.Identificacao.BranchId, input.BranchId);
            Assert.Equal(invoice.Identificacao.DataEmissao, input.CreatedAt);
            Assert.Equal(invoice.Identificacao.DataLancamento, input.UpdatedAt);
            #endregion HEADER
            #region VALORES
            Assert.Equal(util.GetTaxTypeB1Sum("-10",invoice), input.Valores.Cofins);
            #endregion VALORES
        }

    }
}
