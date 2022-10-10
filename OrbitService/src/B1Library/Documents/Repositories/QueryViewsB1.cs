using B1Library.Utilities;
using System.Text;

namespace B1Library.Documents.Repositories
{
    
    public class QueryViewsB1
    {
        private Util util;
        public int codigoIntegracaoOrbit { get; set; }
        public string invoiceCANCELED { get; set; }
        public QueryViewsB1()
        {
            util = new Util();
        }
        public string ReturnQueryB1InboundOtherDocuments(string NameViewB1)
        {
            string Query = string.Empty;
            switch (NameViewB1)
            {
                case "ORBIT_INVOICE_OUT_VW":
                    Query = util.getTableQueryCommandOtherDocuments(NameViewB1);
                    break;
                case "ORBIT_DELIVERY_OUT_VW":
                    Query = util.getTableQueryCommandOtherDocuments(NameViewB1);
                    break;
                case "ORBIT_PURCHASEINVOICE_OUT_VW":
                    Query = util.getTableQueryCommandOtherDocuments(NameViewB1);
                    break;
                case "ORBIT_PURCHASEDELIVERYNOTES_OUT_VW":
                    Query = util.getTableQueryCommandOtherDocuments(NameViewB1);
                    break;
                default:
                    break;
            }
            return Query;
        }
        public string ReturnQueryB1InboundCTe(string DocumentB1)
        {
            string Query = string.Empty;
            switch (DocumentB1)
            {
                case "OPCH":
                    Query = @"SELECT * FROM 
                            ""ORBIT_PURCHASEINVOICE_OUT_VW"" 
                            WHERE  ""ModelInvoice"" 
                            IN ('CT-E') and ""CargaFiscal"" = 0 FOR JSON";
                    break;
                default:
                    break;
            }
            return Query;
        }
        public string ReturnQueryB1InboundNFe(string DocumentB1)
        {
            string Query = string.Empty;
            switch (DocumentB1)
            {
                case "ORBIT_PURCHASEINVOICE_OUT_VW":
                    Query = @"SELECT * FROM 
                            ""ORBIT_PURCHASEINVOICE_OUT_VW"" 
                            WHERE  ""ModeloDocumento"" 
                            IN ('55') and ""CargaFiscal"" = 0 and ""CodInt"" = '0' FOR JSON";
                    break;
                default:
                    break;
            }
            return Query;
        }
        public string ReturnQueryB1InboundNFSe(string DocumentB1)
        {
            string Query = string.Empty;
            switch (DocumentB1)
            {
                case "ORBIT_PURCHASEINVOICE_OUT_VW":
                    Query = @"SELECT * FROM 
                            ""ORBIT_PURCHASEINVOICE_OUT_VW"" 
                            WHERE  ""ModeloDocumento"" 
                            IN ('NFS-e') and ""CargaFiscal"" = 0 and ""CodInt"" = '0' FOR JSON";
                    break;
                default:
                    break;
            }
            return Query;
        }
        public string ReturnQueryB1OutboundNFe(string DocumentB1)
        {
            string Query = string.Empty;
            switch (DocumentB1)
            {
                case "ORBIT_INVOICE_OUT_VW":
                    StringBuilder sb = new StringBuilder();
                    //TODO: update command to dynamic generate table name
                    sb.Append(@$"SELECT * FROM 
                            ""ORBIT_INVOICE_OUT_VW"" 
                            WHERE  ""ModeloDocumento"" 
                            IN ('55') and ""CargaFiscal"" <> 0 ");
                    sb.AppendLine(@$"and ""CodInt"" = '{codigoIntegracaoOrbit}' ");
                    sb.AppendLine(@$"and ""CANCELED"" = '{invoiceCANCELED}' ");
                    sb.AppendLine(@$"FOR JSON");
                    Query = sb.ToString();
                    break;
                case "ODLN":
                    Query = @"SELECT * FROM 
                            ""ORBIT_INVOICE_OUT_VW"" 
                            WHERE  ""ModelInvoice"" 
                            IN ('55') and ""CargaFiscal"" <> 0 FOR JSON";
                    break;
                case "OPDN":
                    Query = @"SELECT * FROM 
                            ""ORBIT_INVOICE_OUT_VW"" 
                            WHERE  ""ModelInvoice"" 
                            IN ('55') and ""CargaFiscal"" <> 0 FOR JSON";
                    break;
                case "OPCH":
                    Query = @"SELECT * FROM 
                            ""ORBIT_INVOICE_OUT_VW"" 
                            WHERE  ""ModelInvoice"" 
                            IN ('55') and ""CargaFiscal"" <> 0 FOR JSON";
                    break;
                case "ORIN":
                    Query = @"SELECT * FROM 
                            ""ORBIT_INVOICE_OUT_VW"" 
                            WHERE  ""ModelInvoice"" 
                            IN ('55') and ""CargaFiscal"" <> 0 FOR JSON";
                    break;
                case "ORDN":
                    Query = @"SELECT * FROM 
                            ""ORBIT_INVOICE_OUT_VW"" 
                            WHERE  ""ModelInvoice"" 
                            IN ('55') and ""CargaFiscal"" <> 0 FOR JSON";
                    break;
                case "ORPC":
                    Query = @"SELECT * FROM 
                            ""ORBIT_INVOICE_OUT_VW"" 
                            WHERE  ""ModelInvoice"" 
                            IN ('55') and ""CargaFiscal"" <> 0 FOR JSON";
                    break;
                case "ORPD":
                    Query = @"SELECT * FROM 
                            ""ORBIT_INVOICE_OUT_VW"" 
                            WHERE  ""ModelInvoice"" 
                            IN ('55') and ""CargaFiscal"" <> 0 FOR JSON";
                    break;
                default:
                    break;
            }
            return Query;
        }
        public string ReturnQueryB1OutboundNFSe(string DocumentB1)
        {
            string Query = string.Empty;
            switch (DocumentB1)
            {
                case "ORBIT_INVOICE_OUT_VW":
                    StringBuilder sb = new StringBuilder();
                    //TODO: update command to dynamic generate table name
                    sb.Append(@$"SELECT * FROM 
                            ""ORBIT_INVOICE_OUT_VW"" 
                            WHERE  ""ModeloDocumento"" 
                            IN ('NFS-e') and ""CargaFiscal"" <> 0 ");
                    sb.AppendLine(@$"and ""CodInt"" = '{codigoIntegracaoOrbit}'");
                    sb.AppendLine(@$"and ""CANCELED"" = '{invoiceCANCELED}' ");
                    sb.AppendLine(@$"FOR JSON");
                    Query = sb.ToString();
                    break;
                default:
                    break;
            }
            return Query;
        }

        public string ReturnQueryB1OutboundNFSeToInutil(string DocumentB1)
        {
            string Query = string.Empty;
            switch (DocumentB1)
            {
                case "ORBIT_INVOICE_OUT_VW":
                    StringBuilder sb = new StringBuilder();
                    //TODO: update command to dynamic generate table name
                    sb.Append(@$"SELECT * FROM 
                            ""ORBIT_INVOICE_OUT_VW"" 
                            WHERE  ""ModeloDocumento"" 
                            IN ('NFS-e') and ""CargaFiscal"" <> 0 ");
                    sb.AppendLine(@$"and ""CANCELED"" = '{invoiceCANCELED}' ");
                    sb.AppendLine(@$"FOR JSON");
                    Query = sb.ToString();
                    break;
                default:
                    break;
            }
            return Query;
        }

        public string ReturnQueryB1OutboundNFSeToCancel(string DocumentB1)
        {
            string Query = string.Empty;
            switch (DocumentB1)
            {
                case "ORBIT_INVOICE_OUT_VW":
                    break;
                default:
                    break;
            }
            return Query;
        }
    }
}
