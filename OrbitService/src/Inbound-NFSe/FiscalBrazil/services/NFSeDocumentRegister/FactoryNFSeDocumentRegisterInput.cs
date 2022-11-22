using System.Collections.Generic;
using static OrbitService.FiscalBrazil.services.NFSeDocumentRegister.NFServico;

namespace OrbitService.FiscalBrazil.services.NFSeDocumentRegister
{
    public class FactoryNFSeDocumentRegisterInput
    {
        public static NFSeDocumentRegisterInput CreateNFSeDocumentRegisterInputInstance()
        {
            NFSeDocumentRegisterInput instance = new NFSeDocumentRegisterInput();
            instance.NFServico = new NFServico();
            instance.NFServico.nfse = new Nfse();
            instance.NFServico.Rps = new Rps();
            instance.NFServico.status = new Status();
            instance.NFServico.Rps.Prestador = new Prestador();
            instance.NFServico.Rps.Prestador.Endereco = new enderecoPrestador();
            instance.NFServico.Rps.Pag = new Pag();
            instance.NFServico.Rps.Pag.DetPag = new List<DetPag>();
            instance.NFServico.Rps.Identificacao = new Identificacao();
            instance.NFServico.Rps.Servico = new Servico();
            instance.NFServico.Rps.Servico.Valores = new Valores();
            instance.NFServico.Rps.Servico.Valores.Iss = new Iss();
            instance.NFServico.Rps.Servico.Valores.Cofins = new Cofins();
            instance.NFServico.Rps.Servico.Valores.Pis = new Pis();
            instance.NFServico.Rps.Servico.Valores.Inss = new Inss();
            instance.NFServico.Rps.Servico.Valores.Ir = new Ir();
            instance.NFServico.Rps.Servico.Valores.Csll = new Csll();
            instance.NFServico.Rps.Servico.Valores.Inss = new Inss();
            instance.NFServico.Rps.Tomador = new Tomador();
            instance.NFServico.Rps.Tomador.Contato = new Contato();
            instance.NFServico.Rps.Tomador.Endereco = new Endereco();
            instance.NFServico.Rps.Intermediario = new Intermediario();            
            return instance;
        }
    }
}
