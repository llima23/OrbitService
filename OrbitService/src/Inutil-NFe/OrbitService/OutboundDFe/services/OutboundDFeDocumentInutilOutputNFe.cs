using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.OutboundDFe.services
{
    public class OutboundDFeDocumentInutilOutputNFe
    {
        public RetInutNFe retInutNFe { get; set; }
        public List<string> communicationIds { get; set; }
        public DocumentsIdsByNnf documentsIdsByNnf { get; set; }
        public string code { get; set; }
        public string message { get; set; }
    }

    public class Attributes
    {
        public string versao { get; set; }
        public string Id { get; set; }
    }

    public class DocumentsIdsByNnf
    {
    }

    public class InfInut
    {
        public Attributes attributes { get; set; }
        public string tpAmb { get; set; }
        public string verAplic { get; set; }
        public string cStat { get; set; }
        public string xMotivo { get; set; }
        public string cUf { get; set; }
        public string ano { get; set; }
        public string cnpj { get; set; }
        public string mod { get; set; }
        public string serie { get; set; }
        public string nNfIni { get; set; }
        public string nNfFin { get; set; }
        public string dhRecbto { get; set; }
        public string nProt { get; set; }
    }

    public class RetInutNFe
    {
        public Attributes attributes { get; set; }
        public InfInut infInut { get; set; }
    }
}
