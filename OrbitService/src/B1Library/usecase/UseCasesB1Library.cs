using System;
using System.Collections.Generic;
using System.Text;

namespace B1Library.usecase
{
    public enum UseCase
    {
        OtherDocuments = 0,
        InboundNFe = 1,
        InboundNFSe = 2,
        InboundCTe = 3,
    }

    public class UseCasesB1Library
    {

        public const string commandOtherDocuments = @"WHERE ""U_TAX4_CodInt"" = '0' 
													and NF.""SeqCode"" < 0 
													and T0.""U_TAX4_CARGAFISCAL"" = 'N'
													and OM.""NfmCode"" IN ('FAT','R','RPA')";

        public const string commandInboundNFe = @"WHERE ""U_TAX4_CodInt"" = '0' 
													and NF.""SeqCode"" < 0 
													and T0.""U_TAX4_CARGAFISCAL"" = 'N'
													and OM.""NfmCode"" IN ('')";

        public const string commandInboundNFSe = @"WHERE ""U_TAX4_CodInt"" = '0' 
													and NF.""SeqCode"" < 0 
													and T0.""U_TAX4_CARGAFISCAL"" = 'N'
													and OM.""NfmCode"" IN ('FAT','R','RPA')";

        public const string commandInboundCTe = @"WHERE ""U_TAX4_CodInt"" = '0' 
													and NF.""SeqCode"" < 0 
													and T0.""U_TAX4_CARGAFISCAL"" = 'N'
													and OM.""NfmCode"" IN ('FAT','R','RPA')";

        public UseCase EnumUseCase { get; set; }
        public string GetCommandUseCase()
        {
            return EnumUseCase switch
            {
                UseCase.OtherDocuments => commandOtherDocuments,
                _ => "",
            };
        }
    }




}
