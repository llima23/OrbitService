using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using _4TAX_Service_Atualiza.Common.Domain;
using _4TAX_Service_Atualiza.Infrastructure;

namespace _4TAX_Service_Atualiza.Common.Handlers
{
    public interface NFSeHandlerODBC
    {

        dynamic GetDocumments();

        List<NFSeB1Object> GetListNFSe();

    }
}
