﻿using B1Library.Documents.Entities;
using System;
using System.Collections.Generic;
using static B1Library.Implementations.Repositories.DBTableNameRepository;

namespace B1Library.Documents
{
    public interface IDocumentsRepository
    {
        List<Invoice> GetInboundOtherDocuments();
        List<Invoice> GetInboundCTe();
        List<Invoice> GetInboundNFe();
        List<Invoice> GetInboundNFSe();
        List<Invoice> GetInboundCce();
        List<Invoice> GetOutboundNFe();
        List<Invoice> GetOutboundNFSe();
        List<Invoice> GetCancelOutboundNFe();
        List<Invoice> GetCancelOutboundNFSe();
        List<Invoice> GetInutilOutboundNFe();
        List<Invoice> GetInutilOutboundNFSe();

        List<Invoice> GetConsultOutboundNFe();
        List<Invoice> GetConsultOutboundNFSe();
        public ConfigEmailAutomatico GetConfigEmail();
        int UpdateDocumentStatus(DocumentStatus documentData, int objType);
       
    }
}
