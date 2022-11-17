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
                Invoice invoice = new Invoice();
                invoice = JsonConvert.DeserializeObject<Invoice>(JsonConvert.SerializeObject(header));

                Logs.InsertLog($"Encontrou o DocEntry: {invoice.DocEntry}");

                #region IDENTIFICACAO


                queryResult = dbRepo.wrapper.ExecuteQuery(setupQueryB1.ReturnCommandIdentificacao(invoice));
                jsonConvert = Convert.ToString(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0]))).Replace("[", "").Replace("]", "");
                invoice.Identificacao = JsonConvert.DeserializeObject<Identificacao>(jsonConvert);

                Logs.InsertLog($"Preencheu o objeto IDENTIFICACAO");
                #endregion IDENTIFICACAO
                #region PARCEIRO
                queryResult = dbRepo.wrapper.ExecuteQuery(setupQueryB1.ReturnCommandParceiroHANA1(invoice));
                jsonConvert = Convert.ToString(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0]))).Replace("[", "").Replace("]", "");
                invoice.Parceiro = JsonConvert.DeserializeObject<Parceiro>(jsonConvert);

                Logs.InsertLog($"Preencheu o objeto PARCEIRO");
                #endregion PARCEIRO
                #region FILIAL
                queryResult = dbRepo.wrapper.ExecuteQuery(setupQueryB1.ReturnCommandFilial(invoice));
                jsonConvert = Convert.ToString(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0]))).Replace("[", "").Replace("]", "");
                invoice.Filial = JsonConvert.DeserializeObject<Filial>(jsonConvert);

                Logs.InsertLog($"Preencheu o objeto FILIAL");
                #endregion FILIAL
                #region HEADER LINHA
                queryResult = dbRepo.wrapper.ExecuteQuery(setupQueryB1.ReturnCommandHeaderLinha(invoice));
                jsonConvert = Convert.ToString(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0])));
                invoice.CabecalhoLinha = JsonConvert.DeserializeObject<List<CabecalhoLinha>>(jsonConvert);
                foreach (CabecalhoLinha cabecalhoLinha in invoice.CabecalhoLinha)
                {
                    #region IMPOSTO LINHA
                    queryResult = dbRepo.wrapper.ExecuteQuery(setupQueryB1.ReturnCommandImpostoLinha(cabecalhoLinha));
                    jsonConvert = Convert.ToString(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0])));
                    cabecalhoLinha.ImpostoLinha = JsonConvert.DeserializeObject<List<ImpostoLinha>>(jsonConvert);

                    Logs.InsertLog($"Preencheu o objeto  IMPOSTO LINHA");
                    #endregion IMPOSTO LINHA

                    #region RETENCAO IMPOSTO LINHA
                    queryResult = dbRepo.wrapper.ExecuteQuery(setupQueryB1.ReturnCommandRetencoesLinha(cabecalhoLinha));
                    jsonConvert = Convert.ToString(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0])));
                    cabecalhoLinha.ImpostoRetidoLinha = JsonConvert.DeserializeObject<List<ImpostoRetidoLinha>>(jsonConvert);

                    Logs.InsertLog($"Preencheu o objeto  RETENCAO IMPOSTO LINHA");

                    #endregion RETENCAO IMPOSTO LINHA
                    #region DESPESA ADICIONAL
                    queryResult = dbRepo.wrapper.ExecuteQuery(setupQueryB1.ReturnCommandDespesaAdicional(cabecalhoLinha));
                    jsonConvert = Convert.ToString(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0])));
                    cabecalhoLinha.DespesaAdicional = JsonConvert.DeserializeObject<List<DespesaAdicional>>(jsonConvert);


                    Logs.InsertLog($"Preencheu o objeto  DESPESA ADICIONAL");
                    #endregion DESPESA ADICIONAL
                    #region DADOS DI
                    queryResult = dbRepo.wrapper.ExecuteQuery(setupQueryB1.ReturnCommandDadosDI(cabecalhoLinha));
                    jsonConvert = Convert.ToString(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0])));
                    cabecalhoLinha.DadosDI = JsonConvert.DeserializeObject<List<DadosDI>>(jsonConvert);

                    Logs.InsertLog($"Preencheu o objeto  DADOS DI");
                    #endregion DADOS DI
                }
                #endregion HEADER LINHA
                #region DUPLICATA
                queryResult = dbRepo.wrapper.ExecuteQuery(setupQueryB1.ReturnCommandDuplicata(invoice));
                jsonConvert = Convert.ToString(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0])));
                invoice.Duplicata = JsonConvert.DeserializeObject<List<Duplicata>>(jsonConvert);


                Logs.InsertLog($"Preencheu o objeto  DUPLICATA");
                #endregion DUPLICATA 
                #region DOCREF
                queryResult = dbRepo.wrapper.ExecuteQuery(setupQueryB1.ReturnCommandDocRef(invoice));
                jsonConvert = Convert.ToString(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0])));
                invoice.DocRef = JsonConvert.DeserializeObject<List<DocRef>>(jsonConvert);

                Logs.InsertLog($"Preencheu o objeto  DOCREF");
                #endregion DOCREF 
                listInvoice.Add(invoice);
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