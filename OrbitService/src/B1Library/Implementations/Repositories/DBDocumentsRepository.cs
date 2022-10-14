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
using B1Library.Documents.Entities;
using static B1Library.Implementations.Repositories.DBTableNameRepository;
using B1Library.usecase;

namespace B1Library.Implementations.Repositories
{
    public class DBDocumentsRepository : IDocumentsRepository
    {
        public string DataBaseName { get; set; }
        public string DataBaseType { get; set; }

        public IWrapper wrapper;
        private UtilDbRepository util;
        private DBTableNameRepository dBTableNameRepository;
        public DBDocumentsRepository(IWrapper wrapper)
        {
            this.wrapper = wrapper;
            util = new UtilDbRepository();
            dBTableNameRepository = new DBTableNameRepository();
            this.DataBaseName = wrapper.DataBaseName;
            this.DataBaseType = wrapper.DataBaseType;
        }

        public List<Invoice> GetInboundOtherDocuments()
        {
            List<Invoice> invoices = new List<Invoice>();
            foreach (TableName tableName in dBTableNameRepository.tableNamesOtherDocuments)
            {
                SetupQueryB1 setupQueryB1 = new SetupQueryB1(this, tableName, new UseCasesB1Library(UseCase.OtherDocuments));
                util.addInvoiceEntriesToList(invoices, wrapper.ExecuteQuery(setupQueryB1.SetupQueryB1SendDocumentToOrbit()));
            }
            return invoices;
        }

        public List<Invoice> GetInboundCTe()
        {
            List<Invoice> invoices = new List<Invoice>();
            foreach (TableName tableName in dBTableNameRepository.tableNamesInbound)
            {
                SetupQueryB1 setupQueryB1 = new SetupQueryB1(this, tableName, new UseCasesB1Library(UseCase.InboundCTe));
                util.addInvoiceEntriesToList(invoices, wrapper.ExecuteQuery(setupQueryB1.SetupQueryB1SendDocumentToOrbit()));
            }
            return invoices;
        }

        public List<Invoice> GetInboundNFe()
        {
            List<Invoice> invoices = new List<Invoice>();
            foreach (TableName tableName in dBTableNameRepository.tableNamesInbound)
            {
                SetupQueryB1 setupQueryB1 = new SetupQueryB1(this, tableName, new UseCasesB1Library(UseCase.InboundNFe));
                util.addInvoiceEntriesToList(invoices, wrapper.ExecuteQuery(setupQueryB1.SetupQueryB1SendDocumentToOrbit()));
            }
            return invoices;
        }

        public List<Invoice> GetInboundNFSe()
        {
            List<Invoice> invoices = new List<Invoice>();
            foreach (TableName tableName in dBTableNameRepository.tableNamesInbound)
            {
                SetupQueryB1 setupQueryB1 = new SetupQueryB1(this, tableName, new UseCasesB1Library(UseCase.InboundNFSe));
                util.addInvoiceEntriesToList(invoices, wrapper.ExecuteQuery(setupQueryB1.SetupQueryB1SendDocumentToOrbit()));
            }
            return invoices;
        }
        public List<Invoice> GetOutboundNFe()
        {
            List<Invoice> invoices = new List<Invoice>();
            foreach (TableName tableName in dBTableNameRepository.tableNamesOutboundNFe)
            {
                SetupQueryB1 setupQueryB1 = new SetupQueryB1(this, tableName, new UseCasesB1Library(UseCase.OutboundNFe));
                util.addInvoiceEntriesToList(invoices, wrapper.ExecuteQuery(setupQueryB1.SetupQueryB1SendDocumentToOrbit()));
            }
            return invoices;

        }

        public List<Invoice> GetOutboundNFSe()
        {
            List<Invoice> invoices = new List<Invoice>();
            foreach (TableName tableName in dBTableNameRepository.tableNamesOutboundNFSe)
            {
                SetupQueryB1 setupQueryB1 = new SetupQueryB1(this, tableName, new UseCasesB1Library(UseCase.OutboundNFSe));
                util.addInvoiceEntriesToList(invoices, wrapper.ExecuteQuery(setupQueryB1.SetupQueryB1SendDocumentToOrbit()));
            }
            return invoices;
        }


        public List<Invoice> GetCancelOutboundNFe()
        {
            List<Invoice> invoices = new List<Invoice>();
            foreach (TableName tableName in dBTableNameRepository.tableNamesOutboundNFSe)
            {
                SetupQueryB1 setupQueryB1 = new SetupQueryB1(this, tableName, new UseCasesB1Library(UseCase.CancelOutboundNFe));
                util.addInvoiceEntriesToList(invoices, wrapper.ExecuteQuery(setupQueryB1.SetupQueryB1SendDocumentToOrbit()));
            }
            return invoices;
        }

        public List<Invoice> GetCancelOutboundNFSe()
        {
            List<Invoice> invoices = new List<Invoice>();
            foreach (TableName tableName in dBTableNameRepository.tableNamesOutboundNFSe)
            {
                SetupQueryB1 setupQueryB1 = new SetupQueryB1(this, tableName, new UseCasesB1Library(UseCase.CancelOutboundNFSe));
                util.addInvoiceEntriesToList(invoices, wrapper.ExecuteQuery(setupQueryB1.SetupQueryB1SendDocumentToOrbit()));
            }
            return invoices;
        }

        public List<Invoice> GetInutilOutboundNFSe()
        {
            List<Invoice> invoices = new List<Invoice>();
            foreach (TableName tableName in dBTableNameRepository.tableNamesOutboundNFSe)
            {
                SetupQueryB1 setupQueryB1 = new SetupQueryB1(this, tableName, new UseCasesB1Library(UseCase.CancelOutboundNFSe));
                util.addInvoiceEntriesToList(invoices, wrapper.ExecuteQuery(setupQueryB1.SetupQueryB1SendDocumentToOrbit()));
            }
            return invoices;
        }

        public List<Invoice> GetConsultOutboundNFe()
        {
            List<Invoice> invoices = new List<Invoice>();
            foreach (TableName tableName in dBTableNameRepository.tableNamesOutboundNFe)
            {
                SetupQueryB1 setupQueryB1 = new SetupQueryB1(this, tableName, new UseCasesB1Library(UseCase.ConsultaNFe));
                util.addInvoiceEntriesToList(invoices, wrapper.ExecuteQuery(setupQueryB1.SetupQueryB1SendDocumentToOrbit()));
            }
            return invoices;
        }

        public List<Invoice> GetConsultOutboundNFSe()
        {
            List<Invoice> invoices = new List<Invoice>();
            foreach (TableName tableName in dBTableNameRepository.tableNamesOutboundNFSe)
            {
                SetupQueryB1 setupQueryB1 = new SetupQueryB1(this, tableName, new UseCasesB1Library(UseCase.ConsultaNFSe));
                util.addInvoiceEntriesToList(invoices, wrapper.ExecuteQuery(setupQueryB1.SetupQueryB1SendDocumentToOrbit()));
            }
            return invoices;
        }



        public int UpdateDocumentStatus(DocumentStatus documentData)
        {
            string updateCommandToDocument = util.generateUpdateDocumentCommand(documentData);
            return wrapper.ExecuteNonQuery(updateCommandToDocument);
        }
    }
}
