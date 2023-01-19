using System;
using System.Collections.Generic;
using System.Text;

namespace _4TAX_Service.Services.Document.Properties
{
    public class EmitResponse
    {
        public struct EmitSuccessResponseOutput
        {
            public string nfseId;

            public string message;
        }

        public struct EmitFailResponseOutput
        {
            public class Error
            {
                public List<string> value { get; set; }
                public string msg { get; set; }
                public string param { get; set; }
                public string location { get; set; }

            }

            public int code { get; set; }
            public string message { get; set; }
            public List<Error> errors { get; set; }

        }
    }
}
