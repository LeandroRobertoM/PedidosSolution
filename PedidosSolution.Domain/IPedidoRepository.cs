using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PedidosSolution.Domain.Pedido;

namespace PedidosSolution.Domain
{
    public interface IPedidoRepository
    {
        void CadastrarPedido(Pedido pedido);
        void EditarPedido(Pedido pedido);
        List<Pedido> BuscarTodos();

        List<Pedido> BuscarTodosAtivo();
        Pedido BuscarPorID(int idpedido);

        Pedido BuscarPorIDStatus(int idpedido);
        Pedido BuscarDescricao(string descricao);
        void DeletarProduto(int idpedido);

        void AtualizaStatus(int idpedido, Status status);
    }
}
