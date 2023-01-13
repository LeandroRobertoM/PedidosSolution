using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PedidosSolution.Domain;
using static PedidosSolution.Domain.Pedido;

namespace PedidosSolution.Infra.Data
{
    public class PedidoRepository : IPedidoRepository
    {
        static PedidoDao _PedidoDao = new PedidoDao();

        public void CadastrarPedido(Pedido pedido)
        {
            var Pedido = pedido;
            _PedidoDao.AdicionarPedido(Pedido);
        }

        public void EditarPedido(Pedido pedido)
        {
            var Pedido = pedido;
            _PedidoDao.AtualizarPedido(Pedido);

        }

        public void DeletarProduto(int idpedido)
        {
            _PedidoDao.DeletarPedido(idpedido);
        }

        public List<Pedido> BuscarTodos()
        {
            return _PedidoDao.BuscaTodos();
        }

        public List<Pedido> BuscarTodosAtivo()
        {
            return _PedidoDao.BuscaTodos();
        }


        public Pedido BuscarPorID(int idpedido)
        {
            return _PedidoDao.BuscarPorId(idpedido);
        }
        public Pedido BuscarPorIDStatus(int idpedido)
        {
            return _PedidoDao.BuscarPorIdStatusPedido(idpedido);
        }

        public Pedido BuscarDescricao(string descricao)
        {
            return _PedidoDao.BuscaPorTexto(descricao);
        }

        public void AtualizaStatus(int idpedido, Status status)
        {

            _PedidoDao.AtualizarStatusPedido(idpedido, status);
        }
    }
}
