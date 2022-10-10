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
        }
        public Status status { get; set; }
    }
    public class Status
    {
        public string cStat { get; set; }
        public string mStat { get; set; }
    }

}
