using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.FiscalBrazil.services
{
    public class InboundCceOutput
    {
        public InboundCceOutput()
        {
            data = new DataOutput();
        }
        public DataOutput data { get; set; }
    }
    public class DataOutput
    {
        public string message { get; set; }
        public string document_id { get; set; }
    }
}
