using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pedidos.Solution.WebApi.Controllers
{
    public class Resposta
    {

        public Resposta(int status, string mensagem)
        {
            this.Status = status;
            this.Mensagem = mensagem;

        }


        public int Status { get; set; }
        public string Mensagem { get; set; }

    }
}

