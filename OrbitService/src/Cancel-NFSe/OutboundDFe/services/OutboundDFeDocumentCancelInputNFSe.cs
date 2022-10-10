using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService_Cancel_NFSe.OutboundDFe.services
{
    public class OutboundDFeDocumentCancelInputNFSe
    {
        public string branchId { get; set; }
        public string nfseId { get; set; }
        public string motivo { get; set; }
        public string code { get; set; }
        public bool soft_cancel { get; set; }
    }
}
