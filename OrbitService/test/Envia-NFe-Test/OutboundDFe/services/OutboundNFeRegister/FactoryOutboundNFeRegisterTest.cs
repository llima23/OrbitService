using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using OrbitService.OutboundDFe.services.OutboundDFeRegister;

namespace OrbitService_Test.OutboundDFe.services.OutboundNFeRegister
{
    public class FactoryOutboundNFeRegisterTest
    {
        [Fact]
        public void ShouldInboundNFeRegisterInput()
        {
            OutboundNFeDocumentRegisterInput input = FactoryOutboundNFeRegister.CreateOutboundNFeDocumentRegisterInputInstance();

            Assert.NotNull(input);
            Assert.NotNull(input.identificacao);
            Assert.NotNull(input.Destinatario);
            Assert.NotNull(input.Destinatario.Endereco);
            Assert.NotNull(input.Emitente.Endereco);
            Assert.NotNull(input.Emitente);
            Assert.NotNull(input.total);
            Assert.NotNull(input.total.IcmsTot);
            Assert.NotNull(input.det);
            Assert.NotNull(input.Emails);
            Assert.NotNull(input.transp);
            Assert.NotNull(input.cobr);
            Assert.NotNull(input.pag);
            Assert.NotNull(input.pag.DetPag);
            Assert.NotNull(input.status);
            Assert.NotNull(input.eventos);
        }
    }
}
