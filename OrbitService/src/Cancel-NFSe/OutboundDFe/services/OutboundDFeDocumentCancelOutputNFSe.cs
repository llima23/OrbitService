using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService_Cancel_NFSe.OutboundDFe.services
{
    public class OutboundDFeDocumentCancelOutputNFSe
    {
        public OutboundDFeDocumentCancelOutputNFSe()
        {
            alerts = new List<Alert>();
            errors = new List<Error> { };
        }
        public bool success { get; set; }
        public bool async { get; set; }
        public string version { get; set; }
        public bool softEvent { get; set; }
        public string message { get; set; }
        public string processDate { get; set; }
        public List<Alert> alerts { get; set; }
        public List<Error> errors { get; set; }
        public List<string> communicationIds { get; set; }
    }

    public class Alert
    {
        public string code { get; set; }
        public string description { get; set; }
    }

    public class Error
    {
        public string code { get; set; }
        public string description { get; set; }
    }
}
