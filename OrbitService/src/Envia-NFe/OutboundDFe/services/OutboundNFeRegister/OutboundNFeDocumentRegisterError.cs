using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.OutboundDFe.services.OutboundDFeRegister
{

    public class OutboundNFeDocumentRegisterError
    {
        public int code { get; set; }
        public string message { get; set; }
        public string nfeId { get; set; }
     
        public List<Error> errors { get; set; }
    }
    public class Error
    {
        public string value { get; set; }
        public string msg { get; set; }
        public string param { get; set; }
        public string location { get; set; }
    }

}
