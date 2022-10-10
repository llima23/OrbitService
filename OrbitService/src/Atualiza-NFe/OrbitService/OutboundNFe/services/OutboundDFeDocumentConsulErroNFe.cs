using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.OutboundNFe.services
{
    public class OutboundDFeDocumentConsulErroNFe
    {
        public OutboundDFeDocumentConsulErroNFe()
        {
            errors = new List<Error>();
        }

        public int code { get; set; }
        public string message { get; set; }
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
