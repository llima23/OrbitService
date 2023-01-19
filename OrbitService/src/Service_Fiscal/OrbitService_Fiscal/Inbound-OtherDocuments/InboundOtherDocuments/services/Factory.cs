using OrbitService.InboundOtherDocuments.services.Input;
using System.Collections.Generic;

namespace OrbitService.InboundOtherDocuments.services
{
    public class Factory
    {
        public static OtherDocumentRegisterInput CreateOtherDocumentRegisterInputInstance()
        {
            OtherDocumentRegisterInput instance = new OtherDocumentRegisterInput();
            instance.Identificacao = new Identificacao();
            instance.Emitente = new Emitente();
            instance.Emitente.Endereco = new Endereco();
            instance.Destinatario = new Destinatario();
            instance.Destinatario.Endereco = new Endereco();
            instance.Valores = new Valores();
            instance.Itens = new List<Iten>();
            instance.Status = new Status();
            return instance;
        }
    }
}
