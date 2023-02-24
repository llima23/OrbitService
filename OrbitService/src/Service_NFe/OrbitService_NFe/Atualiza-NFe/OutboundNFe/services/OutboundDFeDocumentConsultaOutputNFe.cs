using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.OutboundNFe.services
{
    public class OutboundDFeDocumentConsultaOutputNFe
    {
        public OutboundDFeDocumentConsultaOutputNFe()
        {
            status = new Status();
            eventos = new List<Eventos>();
        }

        public string key { get; set; }
        public List<Eventos> eventos { get; set; }
        public Status status { get; set; }
        public Identificacao identificacao { get; set; }
        public Destinatario destinatario { get; set; }
    }
    public class Identificacao
    {
        public string numeroDocFiscal { get; set; }
        public DateTime dataHoraEmissao { get; set; }
    }
    public class Eventos
    {
        public string protocolo { get; set; }
    }
    public class Status
    {
        public string cStat { get; set; }
        public string mStat { get; set; }
    }
    public class Destinatario
    {
        public string cnpj { get; set; }
        public string cpf { get; set; }
        public string nome { get; set; }
        public string indIeDestinatario { get; set; }
    }

}
