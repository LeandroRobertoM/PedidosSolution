using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosSolution.Domain
{
   public interface IClienteRepository
   {

        void CadastrarCliente(Cliente cliente);
        void EditarCliente(Cliente cliente);
        List<Cliente> BuscarTodos();
        Cliente BuscarPorID(int idcliente);
        Cliente BuscarPorCpf(string cpf);

        void AtualizaPontos(Cliente cliente);
        void DeletarCliente(int idcliente);

    }
}
