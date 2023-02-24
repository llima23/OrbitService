using OrbitLibrary.Data;
using System.Collections.Generic;
using System.Data;
using B1Library.Documents;
using System;
using System.Text.RegularExpressions;
using System.Text;
using B1Library.Utilities;
using B1Library.Applications;
using B1Library.Documents.Entities;
using static B1Library.Implementations.Repositories.DBTableNameRepository;
using B1Library.usecase;
using System.Linq;
using B1Library.mapper;

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
                MapperInvoiceB1ToInvoiceLib mapper = new MapperInvoiceB1ToInvoiceLib(this, setupQueryB1);
                invoices = mapper.ReturnInvoiceB1(invoices);
            }
            return invoices;
        }

        public List<Invoice> GetInboundCTe()
        {
            List<Invoice> invoices = new List<Invoice>();
            foreach (TableName tableName in dBTableNameRepository.tableNamesInbound)
            {
                SetupQueryB1 setupQueryB1 = new SetupQueryB1(this, tableName, new UseCasesB1Library(UseCase.InboundCTe));
                MapperInvoiceB1ToInvoiceLib mapper = new MapperInvoiceB1ToInvoiceLib(this, setupQueryB1);
                invoices = mapper.ReturnInvoiceB1(invoices);
            }
            return invoices;
        }

        public List<Invoice> GetInboundNFe()
        {
            List<Invoice> invoices = new List<Invoice>();
            foreach (TableName tableName in dBTableNameRepository.tableNamesInbound)
            {
                SetupQueryB1 setupQueryB1 = new SetupQueryB1(this, tableName, new UseCasesB1Library(UseCase.InboundNFe));
                MapperInvoiceB1ToInvoiceLib mapper = new MapperInvoiceB1ToInvoiceLib(this, setupQueryB1);
                invoices = mapper.ReturnInvoiceB1(invoices);
            }
            return invoices;
        }

        public List<Invoice> GetInboundNFSe()
        {
            List<Invoice> invoices = new List<Invoice>();
            foreach (TableName tableName in dBTableNameRepository.tableNamesInbound)
            {
                SetupQueryB1 setupQueryB1 = new SetupQueryB1(this, tableName, new UseCasesB1Library(UseCase.InboundNFSe));
                MapperInvoiceB1ToInvoiceLib mapper = new MapperInvoiceB1ToInvoiceLib(this, setupQueryB1);
                invoices = mapper.ReturnInvoiceB1(invoices);
            }
            return invoices;
        }

        public List<Invoice> GetInboundCce()
        {
            List<Invoice> invoices = new List<Invoice>();
            foreach (TableName tableName in dBTableNameRepository.tableNamesInbound)
            {
                SetupQueryB1 setupQueryB1 = new SetupQueryB1(this, tableName, new UseCasesB1Library(UseCase.InboundCce));
                MapperInvoiceB1ToInvoiceLib mapper = new MapperInvoiceB1ToInvoiceLib(this, setupQueryB1);
                invoices = mapper.ReturnInvoiceB1(invoices);
            }
            return invoices;
        }
        public List<Invoice> GetOutboundNFe()
        {
            List<Invoice> listInvoices = new List<Invoice>();
            foreach (TableName tableName in dBTableNameRepository.tableNamesOutboundNFe)
            {
                SetupQueryB1 setupQueryB1 = new SetupQueryB1(this, tableName, new UseCasesB1Library(UseCase.OutboundNFe));
                MapperInvoiceB1ToInvoiceLib mapper = new MapperInvoiceB1ToInvoiceLib(this, setupQueryB1);
                listInvoices = mapper.ReturnInvoiceB1(listInvoices);
            }
            return listInvoices;

        }

        public List<Invoice> GetCancelOutboundNFSe()
        {
            List<Invoice> listInvoices = new List<Invoice>();
            foreach (TableName tableName in dBTableNameRepository.tableNamesOutboundNFSe)
            {
                SetupQueryB1 setupQueryB1 = new SetupQueryB1(this, tableName, new UseCasesB1Library(UseCase.CancelOutboundNFSe));
                MapperInvoiceB1ToInvoiceLib mapper = new MapperInvoiceB1ToInvoiceLib(this, setupQueryB1);
                listInvoices = mapper.ReturnInvoiceB1ToCancel(listInvoices);
            }
            return listInvoices;
        }

        public List<Invoice> GetConsultOutboundNFe()
        {
            List<Invoice> listInvoices = new List<Invoice>();
            foreach (TableName tableName in dBTableNameRepository.tableNamesOutboundNFe)
            {
                SetupQueryB1 setupQueryB1 = new SetupQueryB1(this, tableName, new UseCasesB1Library(UseCase.ConsultaNFe));
                MapperInvoiceB1ToInvoiceLib mapper = new MapperInvoiceB1ToInvoiceLib(this, setupQueryB1);
                listInvoices = mapper.ReturnInvoiceB1ToUpdate(listInvoices);
            }
            return listInvoices;
        }

        public List<Invoice> GetCancelOutboundNFe()
        {
            List<Invoice> listInvoices = new List<Invoice>();
            foreach (TableName tableName in dBTableNameRepository.tableNamesOutboundNFe)
            {
                SetupQueryB1 setupQueryB1 = new SetupQueryB1(this, tableName, new UseCasesB1Library(UseCase.CancelOutboundNFe));
                MapperInvoiceB1ToInvoiceLib mapper = new MapperInvoiceB1ToInvoiceLib(this, setupQueryB1);
                listInvoices = mapper.ReturnInvoiceB1ToCancel(listInvoices);
            }
            return listInvoices;
        }
        public List<Invoice> GetInutilOutboundNFe()
        {
            List<Invoice> listInvoices = new List<Invoice>();
            foreach (TableName tableName in dBTableNameRepository.tableNamesOutboundNFe)
            {
                SetupQueryB1 setupQueryB1 = new SetupQueryB1(this, tableName, new UseCasesB1Library(UseCase.InutilOutboundNFe));
                MapperInvoiceB1ToInvoiceLib mapper = new MapperInvoiceB1ToInvoiceLib(this, setupQueryB1);
                listInvoices = mapper.ReturnInvoiceB1ToCancel(listInvoices);
            }
            return listInvoices;
        }

        public List<Invoice> GetOutboundNFSe()
        {
            List<Invoice> invoices = new List<Invoice>();
            foreach (TableName tableName in dBTableNameRepository.tableNamesOutboundNFSe)
            {
                SetupQueryB1 setupQueryB1 = new SetupQueryB1(this, tableName, new UseCasesB1Library(UseCase.OutboundNFSe));
                //util.addInvoiceEntriesToList(invoices, wrapper.ExecuteQuery(setupQueryB1.SetupQueryB1SendDocumentToOrbit()));
            }
            return invoices;
        }



        public List<Invoice> GetInutilOutboundNFSe()
        {
            List<Invoice> listInvoices = new List<Invoice>();
            foreach (TableName tableName in dBTableNameRepository.tableNamesOutboundNFSe)
            {
                SetupQueryB1 setupQueryB1 = new SetupQueryB1(this, tableName, new UseCasesB1Library(UseCase.InutilOutboundNFSe));
                MapperInvoiceB1ToInvoiceLib mapper = new MapperInvoiceB1ToInvoiceLib(this, setupQueryB1);
                listInvoices = mapper.ReturnInvoiceB1ToCancel(listInvoices);
            }
            return listInvoices;
        }




        public List<Invoice> GetConsultOutboundNFSe()
        {
            List<Invoice> listInvoices = new List<Invoice>();
            foreach (TableName tableName in dBTableNameRepository.tableNamesOutboundNFSe)
            {
                SetupQueryB1 setupQueryB1 = new SetupQueryB1(this, tableName, new UseCasesB1Library(UseCase.ConsultaNFSe));
                MapperInvoiceB1ToInvoiceLib mapper = new MapperInvoiceB1ToInvoiceLib(this, setupQueryB1);
                listInvoices = mapper.ReturnInvoiceB1ToUpdate(listInvoices);
            }
            return listInvoices;
        }



        public int UpdateDocumentStatus(DocumentStatus documentData, int objType)
        {
            string updateCommandToDocument = string.Empty;
            foreach (TableName item in dBTableNameRepository.tableNamesOutboundNFe.Where(t => t.ObjB1Type == objType))
            {
                updateCommandToDocument = util.generateUpdateDocumentCommand(documentData, item);
            }

            return wrapper.ExecuteNonQuery(updateCommandToDocument);
        }

        public ConfigEmailAutomatico GetConfigEmail()
        {
            SetupQueryB1 setupQueryB1 = new SetupQueryB1(this);
            MapperInvoiceB1ToInvoiceLib mapper = new MapperInvoiceB1ToInvoiceLib(this,setupQueryB1);
            ConfigEmailAutomatico configEmailAutomatico = mapper.ReturnConfigEmail();
            return configEmailAutomatico;
        }
    }
}
