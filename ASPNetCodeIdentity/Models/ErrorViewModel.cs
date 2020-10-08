using System;

namespace ASPNetCodeIdentity.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        //Adicionando propriedades para o meu tratamento de erro personalizado (os dados acima [default] sao opcionais, podendo ser removidos)

        public int ErrCode { get; set; }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }

    }
}
