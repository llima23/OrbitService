using _4TAX_Service.Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using static _4TAX_Service.Services.Document.Properties.Emit;

namespace OrbitService_NFSe.Envia_NFSe.Application
{
    public class ValidaObjetoOrbit
    {
        public void ValidaObjeto(EmitRequestInput input, NFSeB1Object nfse)
        {
            if(input.rps.identificacao.naturezaOperacao == "0")
            {
                throw new Exception("Campo Natureza Operação NFS-e não preenchido.");
            }
            if (string.IsNullOrEmpty(input.rps.identificacao.regimeEspecialTributacao) && input.rps.identificacao.naturezaOperacao == "0")
            {
                throw new Exception("Campo Tributação nfs-e não preenchido.");
            }
            if (string.IsNullOrEmpty(input.rps.tomador.cnpj) && string.IsNullOrEmpty(input.rps.tomador.cpf) && string.IsNullOrEmpty(input.rps.tomador.inscricaoMunicipal) && input.rps.tomador.endereco != null)
            {
                throw new Exception("Ident.fiscais - CNPJ, CPF ou Inscrição municipal do tomador não preenchido");
            }
            if (nfse.ItemClass != "1")
            {
                throw new Exception("Classificação do Item para imposto diferente de Serviço.");
            }
        }
    }
}
