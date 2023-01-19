using OrbitService.InboundNFe.services.InboundNFeRegister;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OrbitService_Test.FiscalBrasil.services.InboundNFeRegister
{
    public class FactoryInboundNFeRegisterTest
    {
        [Fact]
        public void ShouldInboundNFeRegisterInput()
        {
            InboundNFeDocumentRegisterInput input = FactoryInboundNFeRegister.CreateInboundNFeDocumentRegisterInputInstance();
            
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
