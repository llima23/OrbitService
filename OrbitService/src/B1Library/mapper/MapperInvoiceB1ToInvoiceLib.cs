using B1Library.Documents;
using B1Library.Documents.Entities;
using B1Library.Implementations.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using B1Library.Applications;

namespace B1Library.mapper
{
    public class MapperInvoiceB1ToInvoiceLib
    {
        private DBDocumentsRepository dbRepo;
        private SetupQueryB1 setupQueryB1;
        private string jsonConvert;
        private Invoice invoice;
        private string messageAux;

        public MapperInvoiceB1ToInvoiceLib(DBDocumentsRepository dbRepo, SetupQueryB1 setupQueryB1)
        {
            this.dbRepo = dbRepo;
            this.setupQueryB1 = setupQueryB1;
        }
        public List<Invoice> ReturnInvoiceB1(List<Invoice> listInvoice)
        {
            DataSet queryResult = dbRepo.wrapper.ExecuteQuery(setupQueryB1.ReturnCommandHeader());
            dynamic result = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0]));
            foreach (var header in result)
            {
                try
                {
                    Invoice invoice = new Invoice();
                    invoice = JsonConvert.DeserializeObject<Invoice>(JsonConvert.SerializeObject(header));
                    this.invoice = invoice;
                    ;
                    #region IDENTIFICACAO


                    queryResult = dbRepo.wrapper.ExecuteQuery(setupQueryB1.ReturnCommandIdentificacao(invoice));
                    jsonConvert = Convert.ToString(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0]))).Replace("[", "").Replace("]", "");
                    invoice.Identificacao = JsonConvert.DeserializeObject<Identificacao>(jsonConvert);


                    #endregion IDENTIFICACAO

                    messageAux += "\r" + "IDENTIFICAÇÃO PREENCHIDO";
                    #region PARCEIRO
                    queryResult = dbRepo.wrapper.ExecuteQuery(setupQueryB1.ReturnCommandParceiroHANA1(invoice));
                    jsonConvert = Convert.ToString(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0]))).Replace("[", "").Replace("]", "");
                    invoice.Parceiro = JsonConvert.DeserializeObject<Parceiro>(jsonConvert);


                    #endregion PARCEIRO
                    messageAux += "\r" + "\r" + "PARCEIRO PREENCHIDO";
                    #region FILIAL
                    queryResult = dbRepo.wrapper.ExecuteQuery(setupQueryB1.ReturnCommandFilial(invoice));
                    jsonConvert = Convert.ToString(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0]))).Replace("[", "").Replace("]", "");
                    invoice.Filial = JsonConvert.DeserializeObject<Filial>(jsonConvert);


                    #endregion FILIAL
                    messageAux += "\r" + "FILIAL PREENCHIDO";
                    #region TRANSPORTADORA
                    queryResult = dbRepo.wrapper.ExecuteQuery(setupQueryB1.ReturnCommandTransportadora(invoice));
                    jsonConvert = Convert.ToString(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0]))).Replace("[", "").Replace("]", "");
                    invoice.Transportadora = JsonConvert.DeserializeObject<Transportadora>(jsonConvert);
                    #endregion
                    messageAux += "\r" + "TRANSPORTADORA PREENCHIDO";
                    #region HEADER LINHA
                    queryResult = dbRepo.wrapper.ExecuteQuery(setupQueryB1.ReturnCommandHeaderLinha(invoice));
                    jsonConvert = Convert.ToString(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0])));
                    invoice.CabecalhoLinha = JsonConvert.DeserializeObject<List<CabecalhoLinha>>(jsonConvert);
                    messageAux += "\r" + "CABEÇALHO LINHA PREENCHIDO";
                    foreach (CabecalhoLinha cabecalhoLinha in invoice.CabecalhoLinha)
                    {
                        #region IMPOSTO LINHA
                        queryResult = dbRepo.wrapper.ExecuteQuery(setupQueryB1.ReturnCommandImpostoLinha(cabecalhoLinha));
                        jsonConvert = Convert.ToString(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0])));
                        cabecalhoLinha.ImpostoLinha = JsonConvert.DeserializeObject<List<ImpostoLinha>>(jsonConvert);


                        #endregion IMPOSTO LINHA
                        messageAux += "\r" + "IMPOSTO LINHA PREENCHIDO";
                        #region RETENCAO IMPOSTO LINHA
                        queryResult = dbRepo.wrapper.ExecuteQuery(setupQueryB1.ReturnCommandRetencoesLinha(cabecalhoLinha));
                        jsonConvert = Convert.ToString(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0])));
                        cabecalhoLinha.ImpostoRetidoLinha = JsonConvert.DeserializeObject<List<ImpostoRetidoLinha>>(jsonConvert);

                        #endregion RETENCAO IMPOSTO LINHA
                        messageAux += "\r" + "RETENCAO IMPOSTO LINHA PREENCHIDO";
                        #region DESPESA ADICIONAL
                        queryResult = dbRepo.wrapper.ExecuteQuery(setupQueryB1.ReturnCommandDespesaAdicional(cabecalhoLinha));
                        jsonConvert = Convert.ToString(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0])));
                        cabecalhoLinha.DespesaAdicional = JsonConvert.DeserializeObject<List<DespesaAdicional>>(jsonConvert);



                        #endregion DESPESA ADICIONAL
                        messageAux += "\r" + "DESPESA ADICIONAL PREENCHIDO";
                        #region DADOS DI
                        queryResult = dbRepo.wrapper.ExecuteQuery(setupQueryB1.ReturnCommandDadosDI(cabecalhoLinha));
                        jsonConvert = Convert.ToString(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0])));
                        cabecalhoLinha.DadosDI = JsonConvert.DeserializeObject<List<DadosDI>>(jsonConvert);


                        #endregion DADOS DI
                        messageAux += "\r" + "DADOS DI PREENCHIDO";
                    }
                    #endregion HEADER LINHA
                    #region DUPLICATA
                    queryResult = dbRepo.wrapper.ExecuteQuery(setupQueryB1.ReturnCommandDuplicata(invoice));
                    jsonConvert = Convert.ToString(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0])));
                    invoice.Duplicata = JsonConvert.DeserializeObject<List<Duplicata>>(jsonConvert);



                    #endregion DUPLICATA 
                    messageAux += "\r" + "DUPLICATA  PREENCHIDO";
                    #region DOCREF
                    queryResult = dbRepo.wrapper.ExecuteQuery(setupQueryB1.ReturnCommandDocRef(invoice));
                    jsonConvert = Convert.ToString(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0])));
                    invoice.DocRef = JsonConvert.DeserializeObject<List<DocRef>>(jsonConvert);


                    #endregion DOCREF 
                    messageAux += "\r" + "DOCREF  PREENCHIDO";
                    #region LISTEMAILS
                    queryResult = dbRepo.wrapper.ExecuteQuery(setupQueryB1.ReturnCommandListEmails(invoice));
                    jsonConvert = Convert.ToString(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0])));
                    invoice.Emails = JsonConvert.DeserializeObject<List<Emails>>(jsonConvert);

                    #endregion LISTEMAILS
                    messageAux += "\r" + "LISTEMAILS  PREENCHIDO";
                    listInvoice.Add(invoice);
                }
                catch (Exception ex)
                {
                    DocumentStatus newStatusData = new DocumentStatus("", "", ex.Message + messageAux, invoice.ObjetoB1, invoice.DocEntry, StatusCode.Erro);
                    dbRepo.UpdateDocumentStatus(newStatusData, invoice.ObjetoB1);
                }
            }
            return listInvoice;
        }
        public List<Invoice> ReturnInvoiceB1ToCancel(List<Invoice> listInvoice)
        {
            DataSet queryResult = dbRepo.wrapper.ExecuteQuery(setupQueryB1.ReturnCommandB1CancelDocumentInOrbit());
            dynamic result = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0]));
            foreach (var header in result)
            {
                Invoice invoice = new Invoice();
                invoice = JsonConvert.DeserializeObject<Invoice>(JsonConvert.SerializeObject(header));

                queryResult = dbRepo.wrapper.ExecuteQuery(setupQueryB1.ReturnCommandIdentificacao(invoice));
                jsonConvert = Convert.ToString(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0]))).Replace("[", "").Replace("]", "");
                invoice.Identificacao = JsonConvert.DeserializeObject<Identificacao>(jsonConvert);

                listInvoice.Add(invoice);
            }
            return listInvoice;
        }
        public List<Invoice> ReturnInvoiceB1ToUpdate(List<Invoice> listInvoice)
        {
            DataSet queryResult = dbRepo.wrapper.ExecuteQuery(setupQueryB1.ReturnCommandB1ConsultDocumentInOrbit());
            dynamic result = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0]));
            foreach (var header in result)
            {
                Invoice invoice = new Invoice();
                invoice = JsonConvert.DeserializeObject<Invoice>(JsonConvert.SerializeObject(header));
                listInvoice.Add(invoice);
            }
            return listInvoice;
        }
    }
}