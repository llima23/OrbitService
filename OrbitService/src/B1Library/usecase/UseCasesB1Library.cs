﻿using System;
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
        ConsultaNFSe = 11,
        InboundCce = 12
    }

    public class UseCasesB1Library
    {
        public UseCase EnumUseCase { get; set; }

        public UseCasesB1Library(UseCase enumUseCase)
        {
            EnumUseCase = enumUseCase;
        }
        public const string commandCce = @"WHERE ""U_TAX4_CodInt"" = '0' 
													and T0.""SeqCode"" < 0 
													and T0.""U_TAX4_CARGAFISCAL"" = 'N'
													and OM.""NfmCode"" IN ('6','28','29','66')";

        public const string commandOtherDocuments = @"WHERE ""U_TAX4_CodInt"" = '0' 
													and T0.""SeqCode"" < 0 
													and T0.""U_TAX4_CARGAFISCAL"" = 'N'
													and OM.""NfmCode"" IN ('FAT','R','RPA')";

        public const string commandInboundNFe = @"WHERE ""U_TAX4_CodInt"" = '0' 
													and T0.""SeqCode"" < 0 
													and T0.""U_TAX4_CARGAFISCAL"" = 'N'
													and OM.""NfmCode"" IN ('55')";

        public const string commandInboundNFSe = @"WHERE ""U_TAX4_CodInt"" = '0' 
													and T0.""SeqCode"" < 0 
													and T0.""U_TAX4_CARGAFISCAL"" = 'N'
													and OM.""NfmCode"" IN ('NFS-e')
                                                    and T0.""CANCELED"" = 'N'
                                                    AND T0.""DocDate"" >= CO.""U_TAX4_DateInt""
                                                    OR
                                                    ""U_TAX4_CodInt"" = '0' 
													and T0.""SeqCode"" > 0 
													and T0.""U_TAX4_CARGAFISCAL"" = 'S'
													and OM.""NfmCode"" IN ('NFS-e')
                                                    and T0.""CANCELED"" = 'N'
                                                    AND T0.""DocDate"" >= CO.""U_TAX4_DateInt""
                                                    OR
                                                    ""U_TAX4_CodInt"" = '7'
													and T0.""SeqCode"" > 0 
													and T0.""U_TAX4_CARGAFISCAL"" = 'S'
													and OM.""NfmCode"" IN ('NFS-e')
                                                    and T0.""CANCELED"" = 'Y'
                                                    AND T0.""DocDate"" >= CO.""U_TAX4_DateInt""
                                                    OR
                                                    ""U_TAX4_CodInt"" = '7'
													and T0.""SeqCode"" < 0 
													and T0.""U_TAX4_CARGAFISCAL"" = 'N'
													and OM.""NfmCode"" IN ('NFS-e')
                                                    and T0.""CANCELED"" = 'Y'
                                                    AND T0.""DocDate"" >= CO.""U_TAX4_DateInt""";

        public const string commandInboundCTe = @"WHERE ""U_TAX4_CodInt"" = '0' 
													and T0.""SeqCode"" < 0 
													and T0.""U_TAX4_CARGAFISCAL"" = 'N'
													and OM.""NfmCode"" IN ('CT-e')";

        public const string commandOutboundNFe = @"WHERE ""U_TAX4_CodInt"" = '0' 
													and NF.""SeqCode"" > 0 
													and T0.""U_TAX4_CARGAFISCAL"" = 'N'
                                                    and T0.""CANCELED"" = 'N'
													and OM.""NfmCode"" IN ('55')";

        public const string commandOutboundNFSe = @"WHERE ""U_TAX4_CodInt"" = '0' 
													and NF.""SeqCode"" > 0 
													and T0.""U_TAX4_CARGAFISCAL"" = 'N'
													and OM.""NfmCode"" IN ('NFS-e')";

        public const string commandCancelOutboundNFe = @" and OM.""NfmCode"" IN ('55') and ""U_TAX4_CodInt"" = '2'";

        public const string commandCancelOutboundNFSe = @" and OM.""NfmCode"" IN ('NFS-e') and ""U_TAX4_CodInt"" = '2' and ""U_TAX4_IdRet"" is not null and ""U_TAX4_IdRet"" <> ''";

        public const string commandInutillOutboundNFe = @" and OM.""NfmCode"" IN ('55') and ""U_TAX4_CodInt"" in ('0','3')";

        public const string commandInutilOutboundNFSe = @" and OM.""NfmCode"" IN ('NFS-e') and ""U_TAX4_CodInt"" in ('0','3') and""U_TAX4_IdRet"" is not null and ""U_TAX4_IdRet"" <> ''";

        public const string commandConsultaNFe = @"WHERE ""U_TAX4_CodInt"" = '1' 
													    and T0.""U_TAX4_IdRet"" <> ''
                                                        and T0.""U_TAX4_IdRet"" is not null
                                                        and OM.""NfmCode"" IN ('55')";

        public const string commandConsultaNFSe = @"WHERE ""U_TAX4_CodInt"" in ('1','4')
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
                UseCase.InutilOutboundNFe => commandInutillOutboundNFe,
                UseCase.InutilOutboundNFSe => commandInutilOutboundNFSe,
                UseCase.ConsultaNFe => commandConsultaNFe,
                UseCase.ConsultaNFSe => commandConsultaNFSe,
                UseCase.InboundCce => commandCce,
                _ => "",
            };
        }
    }




}
