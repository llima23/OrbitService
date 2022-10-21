﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.OutboundDFe.services.OutboundDFeRegister
{
    public class FactoryOutboundNFeRegister
    {
        public static OutboundNFeDocumentRegisterInput CreateOutboundNFeDocumentRegisterInputInstance()
        {
            OutboundNFeDocumentRegisterInput instance = new OutboundNFeDocumentRegisterInput();
            instance.infAdic = new InfAdic();
            instance.det = new List<Det>();
            instance.identificacao = new Identificacao();
            instance.Destinatario = new Destinatario();
            instance.Destinatario.Endereco = new Endereco();
            instance.total = new Total();
            instance.total.IcmsTot = new IcmsTot();
            instance.Emails = new List<string>();
            instance.transp = new Transp();
            instance.cobr = new Cobr();
            instance.pag = new Pag();
            instance.pag.DetPag = new List<DetPag>();
            instance.status = new Status();
            instance.eventos = new List<Evento>();
            instance.Emitente = new Emitente();
            instance.Emitente.Endereco = new Endereco();
            return instance;
        }
    }
}
