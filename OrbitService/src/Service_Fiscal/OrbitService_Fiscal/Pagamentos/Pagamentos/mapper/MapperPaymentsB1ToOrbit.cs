using OrbitService_Fiscal.Pagamentos.Pagamentos.mapper.documents.entities;
using OrbitService_Fiscal.Pagamentos.Pagamentos.service;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService_Fiscal.Pagamentos.Pagamentos.mapper
{
    public class MapperPaymentsB1ToOrbit
    {
        public PagamentosInput Mapper(PaymentsB1 payments)
        {
            PagamentosInput input = new PagamentosInput();
            input.documentId = payments.U_TAX4_IdRet;
            input.documentNumber = payments.Serial;
            input.branchId = payments.U_TAX4_EstabID;
            input.dfe = payments.dfe;
            input.paymentDate = payments.TaxDate;
            input.paymentValue = payments.DocTotal;
            return input;
        }
    }
}
