using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.OutboundNFe.services
{
    public class OutboundDFeDocumentConsultaOutputNFe
    {
        public string key { get; set; }
        public OutboundDFeDocumentConsultaOutputNFe()
        {
            status = new Status();
            eventos = new List<Eventos>();
        }
        public List<Eventos> eventos { get; set; }
        public Status status { get; set; }
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

}
