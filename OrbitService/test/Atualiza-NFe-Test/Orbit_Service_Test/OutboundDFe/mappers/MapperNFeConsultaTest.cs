using B1Library.Documents;
using OrbitService.OutboundNFe.mappers;
using OrbitService.OutboundNFe.services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orbit_Service_Test.OutboundDFe.mappers
{
    public class MapperNFeConsultaTest
    {
        private MapperNFeConsulta cut;
        public MapperNFeConsultaTest()
        {
            cut = new MapperNFeConsulta(); 
        }
        [Fact]
        public void ShouldMapperOutputOrbitToDocumentStatusSucess()
        {
            Invoice invoice = new Invoice();
            OutboundDFeDocumentConsultaOutputNFe output = new OutboundDFeDocumentConsultaOutputNFe();

            invoice.Identificacao.IdRetornoOrbit = "21344";
            output.status.cStat = "100";
            output.status.mStat = "AUTORIZADA";
            invoice.ObjetoB1 = 13;
            invoice.DocEntry = 817;
            DocumentStatus documentStatus = cut.ToDocumentStatusResponseSucessful(invoice, output);

            Assert.Equal(invoice.Identificacao.IdRetornoOrbit, documentStatus.IdOrbit);
            Assert.Equal(output.status.cStat, documentStatus.StatusOrbit);
            Assert.Equal(output.status.mStat, documentStatus.Descricao);
            Assert.Equal(invoice.ObjetoB1, documentStatus.ObjetoB1);
            Assert.Equal(invoice.DocEntry, documentStatus.DocEntry);
            if(output.status.cStat == "100")
            {
                Assert.Equal(StatusCode.Sucess, documentStatus.Status);
            }
        }
        [Fact]
        public void ShouldMapperOutputOrbitToDocumentStatusSucessWithError()
        {
            Invoice invoice = new Invoice();
            OutboundDFeDocumentConsultaOutputNFe output = new OutboundDFeDocumentConsultaOutputNFe();

            invoice.Identificacao.IdRetornoOrbit = "21344";
            output.status.cStat = "897";
            output.status.mStat = "Rejeição: Código numérico em formato inválido";
            invoice.ObjetoB1 = 13;
            invoice.DocEntry = 817;
            DocumentStatus documentStatus = cut.ToDocumentStatusResponseSucessful(invoice, output);

            Assert.Equal(invoice.Identificacao.IdRetornoOrbit, documentStatus.IdOrbit);
            Assert.Equal(output.status.cStat, documentStatus.StatusOrbit);
            Assert.Equal(output.status.mStat, documentStatus.Descricao);
            Assert.Equal(invoice.ObjetoB1, documentStatus.ObjetoB1);
            Assert.Equal(invoice.DocEntry, documentStatus.DocEntry);
            Assert.Equal(StatusCode.Erro, documentStatus.Status);
            
        }

        [Fact]
        public void ShouldMapperOutputOrbitToDocumentStatusErro()
        {
            Invoice invoice = new Invoice();
            OutboundDFeDocumentConsulErroNFe output = new OutboundDFeDocumentConsulErroNFe();
            List<Error> listErrors = new List<Error>();
            Error error = new Error();
            error.msg = "Teste List Erro";
            listErrors.Add(error);
            error.msg = "The field nfeId must be 24 characters";
            listErrors.Add(error);
            output.errors = listErrors;
            output.message = "Bad Request";

            foreach (var item in output.errors)
            {
                output.message += item.msg + " - " + "\r";
            }
            invoice.Identificacao.IdRetornoOrbit = "21344";
            invoice.ObjetoB1 = 13;
            invoice.DocEntry = 817;
            DocumentStatus documentStatus = cut.ToDocumentStatusResponseErro(invoice, output);

            Assert.Equal(invoice.Identificacao.IdRetornoOrbit, documentStatus.IdOrbit);
            Assert.Equal("", documentStatus.StatusOrbit);
            Assert.Equal(output.message, documentStatus.Descricao);
            Assert.Equal(invoice.ObjetoB1, documentStatus.ObjetoB1);
            Assert.Equal(invoice.DocEntry, documentStatus.DocEntry);
            Assert.Equal(StatusCode.Erro, documentStatus.Status);
          
        }
    }
}
