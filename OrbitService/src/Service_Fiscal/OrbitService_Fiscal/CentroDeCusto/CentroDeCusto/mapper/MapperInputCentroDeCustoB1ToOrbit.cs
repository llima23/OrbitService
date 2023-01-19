using AccountService_CentroDeCusto.CentroDeCusto.infrastructure.documents.entities;
using AccountService_CentroDeCusto.CentroDeCusto.service;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_CentroDeCusto.CentroDeCusto.mapper
{
    public class MapperInputCentroDeCustoB1ToOrbit
    {
        public CentroDeCustoInput ToCentroDeCustoRegisterInput(CentroDeCustoB1 account)
        {
            CentroDeCustoInput input = new CentroDeCustoInput();
            input.cost_center_code = account.PrcCode;
            input.description = account.PrcName;
            return input;
        }
    }
}
