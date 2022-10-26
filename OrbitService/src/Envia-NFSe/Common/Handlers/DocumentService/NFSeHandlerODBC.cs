using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using _4TAX_Service.Common.Domain;
using _4TAX_Service.Infrastructure;

namespace _4TAX_Service.Common.Handlers
{
    public interface NFSeHandlerODBC
    {

        dynamic GetHeader();

        dynamic GetLines(string DocEntry);

        dynamic GetLineTaxWithholding(string DocEntry, string LineNum);

        dynamic GetLineTax(string DocEntry, string LineNum);

        List<NFSeB1Object> GetListNFSe();

    }
}
