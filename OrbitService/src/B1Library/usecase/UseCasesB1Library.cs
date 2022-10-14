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
        OutboundNFe = 4,
        OutboundNFSe = 5,
        CancelOutboundNFe = 6,
        CancelOutboundNFSe = 7,
        InutilOutboundNFe = 8,
        InutilOutboundNFSe = 9,
        ConsultaNFe = 10,
        ConsultaNFSe = 11
    }

    public class UseCasesB1Library
    {
        public UseCase EnumUseCase { get; set; }

        public UseCasesB1Library(UseCase enumUseCase)
        {
            EnumUseCase = enumUseCase;
        }

        public const string commandOtherDocuments = @"WHERE ""U_TAX4_CodInt"" = '0' 
													and NF.""SeqCode"" < 0 
													and T0.""U_TAX4_CARGAFISCAL"" = 'N'
													and OM.""NfmCode"" IN ('FAT','R','RPA')";

        public const string commandInboundNFe = @"WHERE ""U_TAX4_CodInt"" = '0' 
													and NF.""SeqCode"" < 0 
													and T0.""U_TAX4_CARGAFISCAL"" = 'N'
													and OM.""NfmCode"" IN ('55')";

        public const string commandInboundNFSe = @"WHERE ""U_TAX4_CodInt"" = '0' 
													and NF.""SeqCode"" < 0 
													and T0.""U_TAX4_CARGAFISCAL"" = 'N'
													and OM.""NfmCode"" IN ('NFS-e')";

        public const string commandInboundCTe = @"WHERE ""U_TAX4_CodInt"" = '0' 
													and NF.""SeqCode"" < 0 
													and T0.""U_TAX4_CARGAFISCAL"" = 'N'
													and OM.""NfmCode"" IN ('CT-e')";

        public const string commandOutboundNFe = @"WHERE ""U_TAX4_CodInt"" = '0' 
													and NF.""SeqCode"" > 0 
													and T0.""U_TAX4_CARGAFISCAL"" = 'N'
													and OM.""NfmCode"" IN ('55')";

        public const string commandOutboundNFSe = @"WHERE ""U_TAX4_CodInt"" = '0' 
													and NF.""SeqCode"" > 0 
													and T0.""U_TAX4_CARGAFISCAL"" = 'N'
													and OM.""NfmCode"" IN ('NFS-e')";

        public const string commandCancelOutboundNFe = @" and OM.""NfmCode"" IN ('55')";

        public const string commandCancelOutboundNFSe = @" and OM.""NfmCode"" IN ('NFS-e')";

        public const string commandConsultaNFe = @"WHERE ""U_TAX4_CodInt"" = '1' 
													    and T0.""U_TAX4_IdRet"" <> ''
                                                        and T0.""U_TAX4_IdRet"" is not null
                                                        and OM.""NfmCode"" IN ('55')";

        public const string commandConsultaNFSe = @"WHERE ""U_TAX4_CodInt"" = '1' 
													    and T0.""U_TAX4_IdRet"" <> ''
                                                        and T0.""U_TAX4_IdRet"" is not null
                                                        and OM.""NfmCode"" IN ('NFS-e')";

        public string GetCommandUseCase()
        {
            return EnumUseCase switch
            {
                UseCase.OtherDocuments => commandOtherDocuments,
                UseCase.InboundNFe => commandInboundNFe,
                UseCase.InboundNFSe => commandInboundNFSe,
                UseCase.InboundCTe => commandInboundCTe,
                UseCase.OutboundNFe => commandOutboundNFe,
                UseCase.OutboundNFSe => commandOutboundNFSe,
                UseCase.CancelOutboundNFe => commandCancelOutboundNFe,
                UseCase.CancelOutboundNFSe => commandCancelOutboundNFSe,
                UseCase.InutilOutboundNFe => commandCancelOutboundNFe,
                UseCase.InutilOutboundNFSe => commandCancelOutboundNFSe,
                UseCase.ConsultaNFe => commandConsultaNFe,
                UseCase.ConsultaNFSe => commandConsultaNFSe,
                _ => "",
            };
        }
    }




}
