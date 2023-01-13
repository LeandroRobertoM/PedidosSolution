using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosSolution.Domain
{
    public interface IProdutoRepository
    {
        void CadastrarProduto(Produto produto);
        void EditarProduto(Produto produto);
        List<Produto> BuscarTodos();
        List<Produto> BuscarProdutosAtivos();
        Produto BuscarPorID(int idproduto);
        Produto BuscarDescricao(string descricao);
        void EntradaESaidaEstoque(Produto produto);

        void DesativaProduto(Produto produtoEntrada);
  
        void DeletarProduto(int idproduto);
    }
}
