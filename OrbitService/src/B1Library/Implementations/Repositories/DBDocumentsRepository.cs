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
        private const string oinv_viewname = "ORBIT_INVOICE_OUT_VW";
        private const string odln_viewname = "ORBIT_DELIVERY_OUT_VW";
        private const string opch_viewname = "ORBIT_PURCHASEINVOICE_OUT_VW";
        private const string opdn_viewname = "ORBIT_PURCHASEDELIVERYNOTES_OUT_VW";


        public string DataBaseName { get; set; }
        public string DataBaseType { get; set; }

        private IWrapper wrapper;
        private Util util;
        private QueryViewsB1 queryViewsB1;

        public DBDocumentsRepository(IWrapper wrapper)
        {
            this.wrapper = wrapper;
            util = new Util();
            queryViewsB1 = new QueryViewsB1();
            this.DataBaseName = wrapper.DataBaseName;
            this.DataBaseType = wrapper.DataBaseType;
        }

        public List<Invoice> GetInboundOtherDocuments()
        {
            List<Invoice> invoices = new List<Invoice>();

            //SELECT OINV DOCUMENTS
            if (VerifyIfViewsExistHANA(oinv_viewname))
            {
                util.addInvoiceEntriesToList(invoices, wrapper.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundOtherDocuments(oinv_viewname)));
            }
            //SELECT ODLN DOCUMENTS
            if (VerifyIfViewsExistHANA(odln_viewname))
            {
                util.addInvoiceEntriesToList(invoices, wrapper.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundOtherDocuments(odln_viewname)));
            }
            //SELECT OPCH DOCUMENTS
            if (VerifyIfViewsExistHANA(opch_viewname))
            {
                util.addInvoiceEntriesToList(invoices, wrapper.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundOtherDocuments(opch_viewname)));
            }
            //SELECT OPDN DOCUMENTS
            if (VerifyIfViewsExistHANA(opdn_viewname))
            {
                util.addInvoiceEntriesToList(invoices, wrapper.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundOtherDocuments(opdn_viewname)));
            }
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
            if (VerifyIfViewsExistHANA(opch_viewname))
            {
                util.addInvoiceEntriesToList(invoices, wrapper.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundNFe(opch_viewname)));
                return invoices;
            }
            else
            {
                return invoices;
            }
        }

        public List<Invoice> GetInboundNFSe()
        {
            List<Invoice> invoices = new List<Invoice>();
            //SELECT OPCH DOCUMENTS

            if (VerifyIfViewsExistHANA(opch_viewname))
            {
                util.addInvoiceEntriesToList(invoices, wrapper.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundNFSe(opch_viewname)));
                return invoices;
            }
            else
            {
                return invoices;
            }
        }
        public List<Invoice> GetOutboundNFe()
        {
            List<Invoice> invoices = new List<Invoice>();
            //SELECT OINV DOCUMENTS
            if (VerifyIfViewsExistHANA(oinv_viewname))
            {
                queryViewsB1.invoiceCANCELED = "N";
                queryViewsB1.codigoIntegracaoOrbit = (int)StatusCode.NotasParaEmitir;
                util.addInvoiceEntriesToList(invoices, wrapper.ExecuteQuery(queryViewsB1.ReturnQueryB1OutboundNFe(oinv_viewname)));
                return invoices;
            }
            else
            {
                return invoices;
            }
        }

        public List<Invoice> GetCancelOutboundNFe()
        {
            List<Invoice> invoices = new List<Invoice>();
            //SELECT OINV DOCUMENTS
            if (VerifyIfViewsExistHANA(oinv_viewname))
            {
                queryViewsB1.invoiceCANCELED = "Y";
                queryViewsB1.codigoIntegracaoOrbit = (int)StatusCode.Sucess;
                util.addInvoiceEntriesToList(invoices, wrapper.ExecuteQuery(queryViewsB1.ReturnQueryB1OutboundNFe(oinv_viewname)));
                return invoices;
            }
            else
            {
                return invoices;
            }
        }

        public List<Invoice> GetCancelOutboundNFSe()
        {
            List<Invoice> invoices = new List<Invoice>();
            //SELECT OINV DOCUMENTS
            if (VerifyIfViewsExistHANA(oinv_viewname))
            {
                util.addInvoiceEntriesToList(invoices, wrapper.ExecuteQuery(queryViewsB1.ReturnQueryB1OutboundNFSeToCancel(oinv_viewname)));
                return invoices;
            }
            else
            {
                return invoices;
            }
        }

        public List<Invoice> GetInutilOutboundNFSe()
        {
            List<Invoice> invoices = new List<Invoice>();
            //SELECT OINV DOCUMENTS
            if (VerifyIfViewsExistHANA(oinv_viewname))
            {
                queryViewsB1.invoiceCANCELED = "Y";
                util.addInvoiceEntriesToList(invoices, wrapper.ExecuteQuery(queryViewsB1.ReturnQueryB1OutboundNFSeToInutil(oinv_viewname)));
                return invoices;
            }
            else
            {
                return invoices;
            }
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

        public bool VerifyIfViewsExistHANA(string ViewName)
        {
            DataSet queryResult = wrapper.ExecuteQuery(@$"select * from ""SYS"".""VIEWS"" where ""SCHEMA_NAME"" = '{DataBaseName}' and ""VIEW_NAME"" = '{ViewName}'");
            if (queryResult.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    
    }
}
