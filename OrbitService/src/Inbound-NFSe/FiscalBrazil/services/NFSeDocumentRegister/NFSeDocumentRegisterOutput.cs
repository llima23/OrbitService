using Newtonsoft.Json;
using System;

namespace OrbitService.FiscalBrazil.services.NFSeDocumentRegister
{
    public class NFSeDocumentRegisterOutput
    {
        public NFSeDocumentRegisterOutput()
        {
            data = new Data();
        }
        public Data data { get; set; }
    }

    public class Data
    {
        public string message { get; set; }
        public string document_id { get; set; }
    }
}
