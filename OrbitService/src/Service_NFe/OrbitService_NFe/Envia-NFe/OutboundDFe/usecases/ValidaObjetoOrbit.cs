using OrbitService.OutboundDFe.services.OutboundDFeRegister;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService_NFe.Envia_NFe.OutboundDFe.usecases
{
    public class ValidaObjetoOrbit
    {
        public void ValidaObjeto(OutboundNFeDocumentRegisterInput input)
        {
            if (string.IsNullOrEmpty(input.pag.DetPag[0].TPag))
            {
                throw new Exception("Campo de usuário 4TAX - Forma de Pagamento não preenchido. Verificar configurações em Formas de pagamento");
            }
            if (string.IsNullOrEmpty(input.identificacao.NaturezaOperacao))
            {
                throw new Exception("Campo 4TAX - Natureza Operação não preenchido. Verificar tabela de Utilização");
            }
            if (string.IsNullOrEmpty(input.Destinatario.Cnpj) && string.IsNullOrEmpty(input.Destinatario.Cpf) && string.IsNullOrEmpty(input.Destinatario.IdEstrangeiro))
            {
                throw new Exception("Ident.fiscais - CNPJ, CPF ou ID de estrangeiro não preenchido");
            }
            if (string.IsNullOrEmpty(input.identificacao.Finalidade))
            {
                throw new Exception("Campo Fin. Da NF-e/NFC-e não preenchido. Deve ser preenchido com as opções entre [1-4]");
            }
        }
    }
}
