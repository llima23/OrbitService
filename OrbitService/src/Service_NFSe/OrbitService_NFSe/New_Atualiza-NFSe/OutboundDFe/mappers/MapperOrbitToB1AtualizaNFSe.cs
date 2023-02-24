using B1Library.Documents;
using OrbitService_NFSe.New_Atualiza_NFSe.OutboundDFe.services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService_NFSe.New_Atualiza_NFSe.OutboundDFe.mappers
{
    public class MapperOrbitToB1AtualizaNFSe
    {

        public DocumentStatus ToDocumentStatusResponseSucessful(Invoice invoice, AtualizaNFSeOutput output)
        {
          return new DocumentStatus(invoice.IdRetornoOrbit, output.status.cStat, output.status.mStat, invoice.ObjetoB1, invoice.DocEntry, GetStatusOrbitToB1(output.status.mStat), "", "",invoice.BaseEntry,"",invoice.ModeloDocumento,output.nfse.codigoVerificacao,output.nfse.numero,output.rps.identificacao.numero);
        }
        public DocumentStatus ToDocumentStatusResponseErro(Invoice invoice, AtualizaNFSeError output)
        {
            foreach (var item in output.errors)
            {
                output.message += item.msg + " - " + "\r";
            }
            return new DocumentStatus(invoice.Identificacao.IdRetornoOrbit, "", output.message, invoice.ObjetoB1, invoice.DocEntry, StatusCode.Erro);
        }

        public StatusCode GetStatusOrbitToB1(string statusOrbit)
        {
            switch (statusOrbit)
            {
                case StatusMessageOrbitList.EmProcesso:
                    return StatusCode.FilaDeEmissao;
                case StatusMessageOrbitList.EnvioEmProcesso:
                    return StatusCode.FilaDeEmissao;
                case StatusMessageOrbitList.RPSHomologado:
                    return StatusCode.Sucess;
                case StatusMessageOrbitList.RPSEmitida:
                    return StatusCode.Sucess;
                case StatusMessageOrbitList.NFSeEmitida:
                    return StatusCode.Sucess;
                case StatusMessageOrbitList.RpsNaoEmitida:
                    return StatusCode.Erro;
                case StatusMessageOrbitList.UnknowError:
                    return StatusCode.Erro;
                case StatusMessageOrbitList.ValidationError:
                    return StatusCode.Erro;
                case StatusMessageOrbitList.UnwantedTwin:
                    return StatusCode.Erro;
                case StatusMessageOrbitList.CancelamentoEmProcesso:
                    return StatusCode.CancelEmProcess;
                case StatusMessageOrbitList.CancelamentoEmProcessoPrefeitura:
                    return StatusCode.CancelEmProcess;
                case StatusMessageOrbitList.Cancelada:
                    return StatusCode.CanceladaSucess;
                case StatusMessageOrbitList.Inutilizada:
                    return StatusCode.InutilizadaSucess;
                default:
                    return StatusCode.Erro;
            }
        }
    }

   
}
