using B1Library.Documents;
using OrbitService.OutboundDFe.mappers;
using OrbitService.OutboundDFe.services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OrbitService_Test.OutboundDFe.mappers
{
    public class MapperInputNFeInutilTest
    {
        private MapperInputNFeInutil cut;
        public MapperInputNFeInutilTest()
        {
            cut = new MapperInputNFeInutil();
        }


        [Fact]
        public void ShouldMapperInvoiceB1ToOrbitInput()
        {
            Invoice invoice = new Invoice();
            invoice.Identificacao.BranchId = "21345";
            invoice.Identificacao.Versao = "4.00";
            invoice.Identificacao.SerieDocumento = "1";
            invoice.Identificacao.NumeroDocumento = "100";
            invoice.Identificacao.Justificativa = "Inutilizacao do Documento";
            invoice.Identificacao.DataEmissao = DateTime.Now;

            OutboundDFeDocumentInutilInputNFe input = cut.MapperInvoiceB1ToOrbitInput(invoice);
            Assert.Equal(invoice.Identificacao.BranchId, input.branchId);
            Assert.Equal(invoice.Identificacao.Versao, input.versao);
            Assert.Equal(invoice.Identificacao.SerieDocumento, input.serie);
            Assert.Equal(invoice.Identificacao.NumeroDocumento, input.nNfIni);
            Assert.Equal(invoice.Identificacao.NumeroDocumento, input.nNfFin);
            Assert.Equal(invoice.Identificacao.Justificativa, input.xJust);
            Assert.Equal(invoice.Identificacao.DataEmissao.ToString("yyyy"), input.ano);
        }
    }
}
