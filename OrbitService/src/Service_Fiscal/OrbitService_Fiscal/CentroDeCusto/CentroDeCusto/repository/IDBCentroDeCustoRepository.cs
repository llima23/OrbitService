using AccountService_CentroDeCusto.CentroDeCusto.infrastructure.documents.entities;
using AccountService_CentroDeCusto.CentroDeCusto.service;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_CentroDeCusto.CentroDeCusto.repository
{
    public interface IDBCentroDeCustoRepository
    {
        public List<CentroDeCustoB1> ReturnListOfAccountToOrbit();
        public int UpdateAccountStatusSucess(CentroDeCustoB1 account, CentroDeCustoOutput output);
        public int UpdateAccountStatusError(CentroDeCustoB1 account, CentroDeCustoError output);
    }
}
