using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService_NFSe.New_Atualiza_NFSe.OutboundDFe.services
{
    public class AtualizaNFSeError
    {
        public AtualizaNFSeError()
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

