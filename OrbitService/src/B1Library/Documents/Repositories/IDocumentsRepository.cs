using System;
using System.Collections.Generic;

namespace B1Library.Documents
{
    public interface IDocumentsRepository
    {
        List<Invoice> GetInboundOtherDocuments();
        List<Invoice> GetInboundCTe();
        List<Invoice> GetInboundNFe();
        List<Invoice> GetInboundNFSe();
        List<Invoice> GetOutboundNFe();
        List<Invoice> GetOutboundNFSe();
        List<Invoice> GetCancelOutboundNFe();

        List<Invoice> GetCancelOutboundNFSe();
        int UpdateDocumentStatus(DocumentStatus documentData);
       
    }
}
