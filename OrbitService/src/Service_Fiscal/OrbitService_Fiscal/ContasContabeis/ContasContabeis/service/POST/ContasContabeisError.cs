using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_ContasContabeis.ContasContabeis.service
{
    public class ContasContabeisError
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<Error> errors { get; set; }
    }
    public class Error
    {
        public string msg { get; set; }
        public string param { get; set; }
        public string location { get; set; }
    }
}
