using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PedidosSolution.Domain;
using PedidosSolution.Infra.Data;

namespace ProdutosSolution.Infra.Data
{
    public class ProdutoRepository : IProdutoRepository
    {
        static ProdutoDao _ProdutoDao = new ProdutoDao();
        public void CadastrarProduto(Produto produto)
        {
            var Produto = produto;
            _ProdutoDao.AdicionarProduto(Produto);
        }

        public void EditarProduto(Produto produto)
        {
            var Produto = produto;
            _ProdutoDao.AtualizarProduto(Produto);
        }

        public List<Produto> BuscarTodos()
        {
            return _ProdutoDao.BuscarProdutos();
        }

        public List<Produto> BuscarProdutosAtivos()
        {
            return _ProdutoDao.BuscarProdutosAtivos();
        }

        public Produto BuscarPorID(int idProduto)
        {

            return _ProdutoDao.BuscarprodutoPorID(idProduto);

        }

        public Produto BuscarDescricao(string descricao)
        {
            return _ProdutoDao.BuscarProdutoPorNome(descricao);
        }

        public void DeletarProduto(int idproduto)
        {
            _ProdutoDao.DeleteProduto(idproduto);
        }

        public void EntradaESaidaEstoque(Produto produto)
        {
             var Produto = produto;
            _ProdutoDao.EntradaESaidaEstoque(Produto);
        }

    

        public void DesativaProduto(Produto produtoEntrada)
        {
           
            _ProdutoDao.DesativaProduto(produtoEntrada);
        }

    }
}
