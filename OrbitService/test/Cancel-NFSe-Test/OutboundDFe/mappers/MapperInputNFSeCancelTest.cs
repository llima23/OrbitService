using B1Library.Documents;
using OrbitService_Cancel_NFSe.OutboundDFe.mappers;
using OrbitService_Cancel_NFSe.OutboundDFe.services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OrbitService_Test_Cancel_NFSe.OutboundDFe.mappers
{
    public class MapperInputNFSeCancelTest
    {

        private MapperInputNFSeCancel cut;

        public MapperInputNFSeCancelTest()
        {
            cut = new MapperInputNFSeCancel();
        }

        [Fact]
        public void ShouldMapperInvoiceB1ToOutboundDFeDocumentCancelInputNFSet()
        {
            Invoice invoice = new Invoice();
            invoice.Identificacao.BranchId = "2134";
            invoice.Identificacao.IdRetornoOrbit = "2134";
            invoice.Identificacao.Justificativa = "Rafeta";

            OutboundDFeDocumentCancelInputNFSe input = cut.MapperInvoiceB1ToOutboundDFeDocumentCancelInputNFSe(invoice);
            input.branchId = invoice.Identificacao.BranchId;
            input.nfseId = invoice.Identificacao.IdRetornoOrbit;
            input.motivo = invoice.Identificacao.Justificativa;
            input.soft_cancel = true;
            Assert.Equal(invoice.Identificacao.BranchId, input.branchId);
            Assert.Equal(invoice.Identificacao.IdRetornoOrbit, input.nfseId);
            Assert.Equal(invoice.Identificacao.Justificativa, input.motivo);
            Assert.True(input.soft_cancel);
        }

        [Fact]
        public void ShouldMapperOutboundDFeDocumentCancelOutputToDocumentStatusB1Sucess()
        {
            Invoice invoice = new Invoice();
            OutboundDFeDocumentCancelOutputNFSe output = new OutboundDFeDocumentCancelOutputNFSe();
            invoice.Identificacao.IdRetornoOrbit = "21344";
            output.success = true;
            output.message = "CANCELAMENTO EFETUADO";
            invoice.ObjetoB1 = 13;
            invoice.DocEntry = 817;
            List<Alert> listAlert = new List<Alert>();
            Alert alert = new Alert();
            alert.code = "400";
            alert.description = "TestAlert";
            listAlert.Add(alert);
            output.alerts = listAlert;
            DocumentStatus documentStatus = cut.ToDocumentStatusResponseSucessful(invoice,output);

            Assert.Equal(invoice.Identificacao.IdRetornoOrbit,documentStatus.IdOrbit);
            Assert.Equal(Convert.ToString(output.success), documentStatus.StatusOrbit);
            Assert.Equal(output.message,documentStatus.Descricao);
            Assert.Equal(invoice.ObjetoB1,documentStatus.ObjetoB1);
            Assert.Equal(invoice.DocEntry,documentStatus.DocEntry);
            Assert.Equal(StatusCode.Sucess,documentStatus.Status);
        }

        [Fact]
        public void ShouldMapperOutboundDFeDocumentCancelOutputToDocumentStatusB1Error()
        {
            Invoice invoice = new Invoice();
            OutboundDFeDocumentCancelOutputNFSe output = new OutboundDFeDocumentCancelOutputNFSe();
            List<Alert> listAlert = new List<Alert>();
            Alert alert = new Alert();
            alert.code = "400";
            alert.description = "TestAlert";
            listAlert.Add(alert);
            output.alerts = listAlert;

            List<Error> listErrors = new List<Error>();
            Error error = new Error();
            error.code = "410";
            error.description = "TestErro";
            listErrors.Add(error);
            output.errors = listErrors;

            invoice.Identificacao.IdRetornoOrbit = "21344";
            output.success = false;
            output.message = "CANCELAMENTO EFETUADO";

            List<string> communicationId = new List<string>();
            communicationId.Add("1234");
            output.communicationIds = communicationId;

            string DescricaoErro = string.Empty;
            foreach (var item in output.errors)
            {
                DescricaoErro += item.description + " - " + "\r";
            }
            foreach (var item in output.alerts)
            {
                DescricaoErro += item.description + " - " + "\r";
            }

            invoice.ObjetoB1 = 13;
            invoice.DocEntry = 817;
            DocumentStatus documentStatus = cut.ToDocumentStatusResponseError(invoice, output);

            Assert.Equal(invoice.Identificacao.IdRetornoOrbit, documentStatus.IdOrbit);
            Assert.Equal(Convert.ToString(output.success), documentStatus.StatusOrbit);
            Assert.Equal(DescricaoErro, documentStatus.Descricao);
            Assert.Equal(invoice.ObjetoB1, documentStatus.ObjetoB1);
            Assert.Equal(invoice.DocEntry, documentStatus.DocEntry);
            Assert.Equal(StatusCode.Erro, documentStatus.Status);
        }

    }
}
