using B1Library.Documents;
using OrbitService.OutboundDFe.mappers;
using OrbitService.OutboundDFe.services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OrbitService_Tst.OutboundDFe.mappers
{
    public class MapperInputNFeCancelTest
    {
        private MapperInputNFeCancel cut;

        public MapperInputNFeCancelTest()
        {
            cut = new MapperInputNFeCancel();
        }
        [Fact]
        public void ShouldMapperInvoiceB1ToOutboundDFeDocumentCancelInputNFSet()
        {
            Invoice invoice = new Invoice();
            invoice.Identificacao.BranchId = "2134";
            invoice.IdRetornoOrbit = "2134";
            invoice.Identificacao.Justificativa = "Rafeta";

            OutboundDFeDocumentCancelInputNFe input = cut.MapperInvoiceB1ToOutboundDFeDocumentCancelInputNFe(invoice);
            Assert.Equal(invoice.IdRetornoOrbit, input.nfeId);
            Assert.Equal(invoice.Identificacao.Justificativa, input.xJust);
        }

        [Fact]
        public void ShouldMapperOutboundDFeDocumentCancelOutputToDocumentStatusB1Sucess()
        {
            Invoice invoice = new Invoice();
            OutboundDFeDocumentCancelOutputNFe output = new OutboundDFeDocumentCancelOutputNFe();
            invoice.IdRetornoOrbit = "21344";
            output.message = "CANCELAMENTO EFETUADO";
            invoice.ObjetoB1 = 13;
            invoice.DocEntry = 817;
            DocumentStatus documentStatus = cut.ToDocumentStatusResponseSucessful(invoice, output);

            Assert.Equal(invoice.IdRetornoOrbit, documentStatus.IdOrbit);
            Assert.Equal("", documentStatus.StatusOrbit);
            Assert.Equal(output.message, documentStatus.Descricao);
            Assert.Equal(invoice.ObjetoB1, documentStatus.ObjetoB1);
            Assert.Equal(invoice.DocEntry, documentStatus.DocEntry);
            Assert.Equal(StatusCode.CanceladaSucess, documentStatus.Status);
        }

        [Fact]
        public void ShouldMapperOutboundDFeDocumentCancelOutputToDocumentStatusB1Error()
        {
            Invoice invoice = new Invoice();
            OutboundDFeDocumentCancelOutputNFe output = new OutboundDFeDocumentCancelOutputNFe();
            invoice.IdRetornoOrbit = "21344";
            output.message = "CANCELAMENTO EFETUADO";
            invoice.ObjetoB1 = 13;
            invoice.DocEntry = 817;
            DocumentStatus documentStatus = cut.ToDocumentStatusResponseError(invoice, output);
            Assert.Equal(invoice.IdRetornoOrbit, documentStatus.IdOrbit);
            Assert.Equal("", documentStatus.StatusOrbit);
            Assert.Equal(output.message, documentStatus.Descricao);
            Assert.Equal(invoice.ObjetoB1, documentStatus.ObjetoB1);
            Assert.Equal(invoice.DocEntry, documentStatus.DocEntry);
            Assert.Equal(StatusCode.Erro, documentStatus.Status);
        }
    }
}
