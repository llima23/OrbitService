using OrbitService_Fiscal.Pagamentos.Pagamentos.mapper.documents.entities;
using OrbitService_Fiscal.Pagamentos.Pagamentos.service;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService_Fiscal.Pagamentos.Pagamentos.repository
{
    public interface IDBPaymentsRepository
    {
        public List<PaymentsB1> ReturnListOfPaymentsToOrbit();
        public int UpdateAccountStatusSucess(PaymentsB1 payment, PagamentosOutput output);
        public int UpdateAccountStatusError(PaymentsB1 payment, PagamentosErro output);
    }
}
