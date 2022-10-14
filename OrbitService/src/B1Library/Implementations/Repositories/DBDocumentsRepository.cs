using OrbitLibrary.Data;
using System.Collections.Generic;
using System.Data;
using B1Library.Documents;
using System;
using System.Text.RegularExpressions;
using System.Text;
using B1Library.Utilities;
using B1Library.Documents.Repositories;
using B1Library.Applications;

namespace B1Library.Implementations.Repositories
{
    public class DBDocumentsRepository : IDocumentsRepository
    {
        public string DataBaseName { get; set; }
        public string DataBaseType { get; set; }

        public IWrapper wrapper;
        private UtilDbRepository util;
        public DBDocumentsRepository(IWrapper wrapper)
        {
            this.wrapper = wrapper;
            util = new UtilDbRepository();
            this.DataBaseName = wrapper.DataBaseName;
            this.DataBaseType = wrapper.DataBaseType;
        }

        public List<Invoice> GetInboundOtherDocuments()
        {
            List<Invoice> invoices = new List<Invoice>();
            return invoices;
        }

        public List<Invoice> GetInboundCTe()
        {
            List<Invoice> invoices = new List<Invoice>();
            return invoices;
        }

        public List<Invoice> GetInboundNFe()
        {
            List<Invoice> invoices = new List<Invoice>();
            //SELECT OPCH DOCUMENTS
            return invoices;
        }

        public List<Invoice> GetInboundNFSe()
        {
            List<Invoice> invoices = new List<Invoice>();
            //SELECT OPCH DOCUMENTS
            return invoices;
            
        }
        public List<Invoice> GetOutboundNFe()
        {
            List<Invoice> invoices = new List<Invoice>();
            //SELECT OINV DOCUMENTS
            return invoices;
            
        }

        public List<Invoice> GetCancelOutboundNFe()
        {
            List<Invoice> invoices = new List<Invoice>();
            return invoices;
        }

        public List<Invoice> GetCancelOutboundNFSe()
        {
            List<Invoice> invoices = new List<Invoice>();
            return invoices;
           
        }

        public List<Invoice> GetInutilOutboundNFSe()
        {
            List<Invoice> invoices = new List<Invoice>();
            return invoices;
        }

        public List<Invoice> GetOutboundNFSe()
        {
            List<Invoice> invoices = new List<Invoice>();
            //SELECT OINV DOCUMENTS
            return invoices;
        }

        public int UpdateDocumentStatus(DocumentStatus documentData)
        {
            string updateCommandToDocument = util.generateUpdateDocumentCommand(documentData);
            return wrapper.ExecuteNonQuery(updateCommandToDocument);
        }
    }
}
