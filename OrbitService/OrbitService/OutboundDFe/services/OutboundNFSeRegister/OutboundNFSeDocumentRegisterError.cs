using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitService.OutboundDFe.services.OutboundNFSeRegister
{
    public class OutboundNFSeDocumentRegisterError
    {
        public int code { get; set; }
        public string message { get; set; }
        public string nfseId { get; set; }

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
