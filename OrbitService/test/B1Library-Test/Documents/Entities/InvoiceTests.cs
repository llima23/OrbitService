using B1Library.Documents;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xunit;

namespace B1Library_Tests.Documents
{
    public class InvoiceTests
    {
        private Invoice cut;

        public InvoiceTests()
        {
            cut = new Invoice();
        }
        [Fact]
        public void ShouldCreateAValidInvoiceB1Instance()
        {
            Assert.NotNull(cut);
            Assert.NotNull(cut.CabecalhoLinha);
            Assert.NotNull(cut.Parceiro);
            Assert.NotNull(cut.Filial);
            Assert.NotNull(cut.Identificacao);
            Assert.NotNull(cut.Duplicata);
        }

        [Fact]
        public void ShouldAddLineItem()
        {
            CabecalhoLinha itemLine = new CabecalhoLinha();

            Assert.NotNull(itemLine.ImpostoLinha);
            Assert.NotNull(itemLine.ImpostoRetidoLinha);

            cut.AddItemLine(itemLine);
            Assert.Single(cut.CabecalhoLinha);
        }

        [Fact]
        public void ShoulNotdAddLineItemWithoutTaxObjects()
        {

            Assert.Throws<ArgumentException>(() => cut.AddItemLine(null));

            CabecalhoLinha itemLine = new CabecalhoLinha();
            itemLine.ImpostoLinha = null;
            Assert.Throws<ArgumentException>(() => cut.AddItemLine(itemLine));

            itemLine = new CabecalhoLinha();
            itemLine.ImpostoRetidoLinha = null;
            Assert.Throws<ArgumentException>(() => cut.AddItemLine(itemLine));
        }


    }
}
