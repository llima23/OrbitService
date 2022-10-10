using B1Library.Documents;
using B1Library.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace B1Library_Tests.Utilities
{
    public class UtilTest
    {
        private Util cut;
        private Invoice invoice;
        public UtilTest()
        {
            cut = new Util();
            invoice = new Invoice();
        }

        [Fact]
        public void ShouldReturnTaxNameSum()
        {
            CabecalhoLinha itemLine = new CabecalhoLinha();
            ImpostoLinha tax = new ImpostoLinha();
            tax.NomeImposto = "PIS";
            tax.TipoImpostoOrbit = "-4";
            tax.ValorImposto = 10.00;
            itemLine.ImpostoLinha.Add(tax);
            invoice.AddItemLine(itemLine);

            itemLine = new CabecalhoLinha();
            tax = new ImpostoLinha();
            tax.NomeImposto = "PIS";
            tax.TipoImpostoOrbit = "-4";
            tax.ValorImposto = 40.00;
            itemLine.ImpostoLinha.Add(tax);
            invoice.AddItemLine(itemLine);

            double sumTaxName = cut.GetTaxTypeB1Sum("-4", invoice);
            Assert.Equal(50.00, sumTaxName);
        }

        [Fact]
        public void ShouldReturnZeroSumWithoutSelectedTaxName()
        {
            double sumTaxName = cut.GetTaxTypeB1Sum("PIS", invoice);
            Assert.Equal(0.00, sumTaxName);

            CabecalhoLinha itemLine = new CabecalhoLinha();
            ImpostoLinha tax = new ImpostoLinha();
            tax.NomeImposto = "ICMS";
            tax.ValorImposto = 10.00;
            itemLine.ImpostoLinha.Add(tax);
            invoice.AddItemLine(itemLine);

            sumTaxName = cut.GetTaxTypeB1Sum("PIS", invoice);
            Assert.Equal(0.00, sumTaxName);
        }

        [Fact]
        public void ShouldReturnNegativeSumToSelectedTaxName()
        {
            CabecalhoLinha itemLine = new CabecalhoLinha();
            ImpostoLinha tax = new ImpostoLinha();
            tax.NomeImposto = "PIS";
            tax.TipoImpostoOrbit = "-4";
            tax.ValorImposto = -10.00;
            itemLine.ImpostoLinha.Add(tax);
            invoice.AddItemLine(itemLine);

            double sumTaxName = cut.GetTaxTypeB1Sum("-4", invoice);
            Assert.Equal(-10.00, sumTaxName);
        }

        [Fact]
        public void ShouldConvertDateB1ToFormatOrbitWithDocTime()
        {
            Identificacao identificacao = new Identificacao();
            identificacao.DataEmissao = new DateTime(2022, 9, 28, 00, 00, 00);
            identificacao.DocTime = "1225";
            string Data = cut.ConvertDateB1ToFormatOrbit(identificacao.DataEmissao, identificacao.DocTime);
            Assert.Equal("2022-09-28T12:00:00-03:00", Data);
        }
        [Fact]
        public void ShouldConvertDateB1ToFormatOrbitWithoutDocTime()
        {
            Identificacao identificacao = new Identificacao();
            identificacao.DataEmissao = new DateTime(2022, 9, 28, 00, 00, 00);
            string Data = cut.ConvertDateB1ToFormatOrbit(identificacao.DataEmissao);
            Assert.Equal("2022-09-28", Data);
        }

        [Fact]
        public void ShouldGetVProdSum()
        {
            List<CabecalhoLinha> listLinha = new List<CabecalhoLinha>();
            CabecalhoLinha itemLine = new CabecalhoLinha();

            itemLine.ValorUnitarioLinha = 38.00;
            itemLine.QuantidadeLinha = 15;
            itemLine.ValorTotalLinnha = 570.00;
            listLinha.Add(itemLine);

            double vprod = cut.GetVProdSum(listLinha);
            Assert.Equal("570.00",cut.ToOrbitString(vprod));
        }

        [Fact]
        public void ShouldGetVBcSum()
        {
            CabecalhoLinha itemLine = new CabecalhoLinha();
            ImpostoLinha tax = new ImpostoLinha();
            tax.NomeImposto = "ICMS";
            tax.TipoImpostoOrbit = "-6";
            itemLine.ImpostoLinha.Add(tax);
            invoice.AddItemLine(itemLine);
            double sumTaxName = cut.GetTaxTypeB1Sum("-6", invoice);
            Assert.Equal("0.00", cut.ToOrbitString(sumTaxName));

        }

        [Fact]
        public void ShouldReturnTaxNameSumWithDeson()
        {
            CabecalhoLinha itemLine = new CabecalhoLinha();
            ImpostoLinha tax = new ImpostoLinha();
            tax.NomeImposto = "ICMS";
            tax.TipoImpostoOrbit = "-6";
            tax.ValorImposto = 10.00;
            tax.SimOuNaoDesoneracao = "Y";
            itemLine.ImpostoLinha.Add(tax);
            invoice.AddItemLine(itemLine);
            
            double sumTaxName = cut.GetTaxTypeB1SumDeson("-6", invoice);
            Assert.Equal(10.00, sumTaxName);
        }

 

        [Fact]
        public void ShouldReturnGetValorSomaDescontoItens()
        {
            List<CabecalhoLinha> listLinha = new List<CabecalhoLinha>();
            CabecalhoLinha itemLine = new CabecalhoLinha();
            itemLine.ValorUnitarioLinha = 38.00;
            itemLine.QuantidadeLinha = 15;
            itemLine.ValorTotalDescontoLinha = 2.695;
            listLinha.Add(itemLine);

            itemLine = new CabecalhoLinha();
            itemLine.ValorUnitarioLinha = 38.00;
            itemLine.QuantidadeLinha = 15;
            itemLine.ValorTotalDescontoLinha = 2.695;
            listLinha.Add(itemLine);

            double vprod = cut.GetValorSomaDescontoItens(listLinha);
            Assert.Equal("5.39", cut.ToOrbitString(vprod));
        }
        [Fact]
        public void ShouldGetTaxTypeB1VImpSumForItem()
        {
            CabecalhoLinha itemLine = new CabecalhoLinha();
            ImpostoLinha tax = new ImpostoLinha();
            tax.NomeImposto = "ICMS";
            tax.TipoImpostoOrbit = "-6";
            tax.ValorImposto = 10.00;
            tax.SimOuNaoDesoneracao = "N";
            itemLine.ImpostoLinha.Add(tax);

            tax = new ImpostoLinha();
            tax.NomeImposto = "ICMS";
            tax.TipoImpostoOrbit = "-6";
            tax.ValorImposto = 35.00;
            tax.SimOuNaoDesoneracao = "N";
            itemLine.ImpostoLinha.Add(tax);
            invoice.AddItemLine(itemLine);


            double sumTaxName = cut.GetTaxTypeB1VImpSumForItem(itemLine.ImpostoLinha, tax.TipoImpostoOrbit);
            Assert.Equal(45.00, sumTaxName);
        }
        [Fact]
        public void ShouldGetTaxTypeB1VBcSumForItem()
        {
            CabecalhoLinha itemLine = new CabecalhoLinha();
            ImpostoLinha tax = new ImpostoLinha();
            tax.NomeImposto = "ICMS";
            tax.TipoImpostoOrbit = "-6";
            tax.ValorBaseImposto = 10.00;
            tax.SimOuNaoDesoneracao = "N";
            itemLine.ImpostoLinha.Add(tax);

            tax = new ImpostoLinha();
            tax.NomeImposto = "ICMS";
            tax.TipoImpostoOrbit = "-6";
            tax.ValorBaseImposto = 35.00;
            tax.SimOuNaoDesoneracao = "N";
            itemLine.ImpostoLinha.Add(tax);
            invoice.AddItemLine(itemLine);


            double sumTaxName = cut.GetTaxTypeB1VBcSumForItem(itemLine.ImpostoLinha, tax.TipoImpostoOrbit);
            Assert.Equal(45.00, sumTaxName);
        }
    }
}
