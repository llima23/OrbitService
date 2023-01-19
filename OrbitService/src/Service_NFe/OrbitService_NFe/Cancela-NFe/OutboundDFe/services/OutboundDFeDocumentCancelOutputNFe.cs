using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.OutboundDFe.services
{
    public class OutboundDFeDocumentCancelOutputNFe
    {
        public RetEnvEvento retEnvEvento { get; set; }
        public List<string> communicationIds { get; set; }

        public string code { get; set; }
        public string message { get; set; }
    }

    public class Attributes
    {
        public string versao { get; set; }
        public string Id { get; set; }
    }



    public class RetEnvEvento
    {
        public Attributes attributes { get; set; }
        public string idLote { get; set; }
        public string tpAmb { get; set; }
        public string verAplic { get; set; }
        public string cOrgao { get; set; }
        public string cStat { get; set; }
        public string xMotivo { get; set; }
        public List<RetEvento> retEvento { get; set; }
    }

    public class RetEvento
    {
        public Attributes attributes { get; set; }
        public InfEvento infEvento { get; set; }
    }

    public class InfEvento
    {
        public Attributes attributes { get; set; }
        public string tpAmb { get; set; }
        public string verAplic { get; set; }
        public string cOrgao { get; set; }
        public string cStat { get; set; }
        public string xMotivo { get; set; }
        public string chNFe { get; set; }
        public string tpEvento { get; set; }
        public string xEvento { get; set; }
        public string nSeqEvento { get; set; }
        public string cOrgaoAutor { get; set; }
        public string emailDest { get; set; }
        public string dhRegEvento { get; set; }
        public string nProt { get; set; }
    }
}
