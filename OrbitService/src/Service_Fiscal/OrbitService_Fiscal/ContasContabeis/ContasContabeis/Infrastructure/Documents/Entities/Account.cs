using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_ContasContabeis.ContasContabeis.Infrastructure.Documents.Entities
{
    public class Account
    {
        public string OriginCode { get; set; }
        public string AcctType { get; set; }
        public string AcctCode { get; set; }
        public string AcctName { get; set; }
        public DateTime CreateDate { get; set; }
        public string FatherNum { get; set; }
        public int Levels { get; set; }
        public string Postable { get; set; }
        public string U_TAX4_LIDO { get; set; }
        public string U_TAX4_IdRet { get; set; }
        public string GroupMask { get; set; }
    }
}
