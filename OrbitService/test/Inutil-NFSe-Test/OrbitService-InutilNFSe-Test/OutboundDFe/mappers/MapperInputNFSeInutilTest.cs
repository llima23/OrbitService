using B1Library.Documents;
using OrbitService.OutboundDFe.mappers;
using OrbitService.OutboundDFe.services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OrbitService_InutilNFSe_Test.OutboundDFe.mappers
{
    public class MapperInputNFSeInutilTest
    {
        private MapperInputNFSeInutil cut;
        public MapperInputNFSeInutilTest()
        {
            cut = new MapperInputNFSeInutil();
        }

        [Fact]
        public void ShouldMapperInvoiceB1ToOrbitInput()
        {
            Invoice invoice = new Invoice();
            invoice.Identificacao.BranchId = "21345";
            invoice.Identificacao.IdRetornoOrbit = "214345";

            OutboundDFeDocumentInutilInputNFSe input = cut.MapperInvoiceB1ToOrbitInput(invoice);
            Assert.Equal(invoice.Identificacao.BranchId, input.branchId);
            Assert.Equal(invoice.Identificacao.IdRetornoOrbit, input.nfseId);
        }

        [Fact]
        public void ShouldMapperOrbitOutputToUpdateB1Sucess()
        {
            Invoice invoice = new Invoice();
            invoice.Identificacao.BranchId = "21345";
            invoice.Identificacao.IdRetornoOrbit = "214345";
            invoice.DocEntry = 12345;
            invoice.ObjetoB1 = 13;
            OutboundDFeDocumentInutilOutputNFSe output = new OutboundDFeDocumentInutilOutputNFSe();
            output.success = true;
            output.message = "INUTILIZADA";
            DocumentStatus documentStatus = cut.MapperOrbitOutputToUpdateB1Sucess(invoice,output);

            Assert.Equal(invoice.Identificacao.IdRetornoOrbit, documentStatus.IdOrbit);
            Assert.Equal(Convert.ToString(output.success), documentStatus.StatusOrbit);
            Assert.Equal(output.message, documentStatus.Descricao);
            Assert.Equal(invoice.ObjetoB1, documentStatus.ObjetoB1);
            Assert.Equal(invoice.DocEntry, documentStatus.DocEntry);
            Assert.Equal(StatusCode.InutilizadaSucess, documentStatus.Status);

        }


        [Fact]
        public void ShouldMapperOrbitOutputToUpdateB1Error()
        {
            Invoice invoice = new Invoice();
            invoice.Identificacao.BranchId = "21345";
            invoice.Identificacao.IdRetornoOrbit = "214345";
            invoice.DocEntry = 12345;
            invoice.ObjetoB1 = 13;
            OutboundDFeDocumentInutilOutputNFSe output = new OutboundDFeDocumentInutilOutputNFSe();
            output.success = false;
            output.message = "INUTILIZADA";
            DocumentStatus documentStatus = cut.MapperOrbitOutputToUpdateB1Error(invoice, output);

            Assert.Equal(invoice.Identificacao.IdRetornoOrbit, documentStatus.IdOrbit);
            Assert.Equal(Convert.ToString(output.success), documentStatus.StatusOrbit);
            Assert.Equal(output.message, documentStatus.Descricao);
            Assert.Equal(invoice.ObjetoB1, documentStatus.ObjetoB1);
            Assert.Equal(invoice.DocEntry, documentStatus.DocEntry);
            Assert.Equal(StatusCode.Erro, documentStatus.Status);

        }
    }
}
