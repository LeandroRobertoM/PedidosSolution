using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosSolution.Domain
{
    public class Produto
    {
        public int Idproduto { get; set; }
        public string Descricao { get; set; }
        public double PrecoUnitario { get; set; }
        public DateTime DataValidade { get; set; }
        public double Quantidade { get; set; }
        public Boolean Ativo { get; set; }


        public Produto(string descricao, double precounitario, DateTime datavalidade, double quantidade, Boolean ativo)
        {
            this.Descricao = descricao;
            this.PrecoUnitario = precounitario;
            this.DataValidade = datavalidade;
            this.Quantidade = quantidade;
            this.Ativo = ativo;
  
        }

        public Produto (){}

 

        public double EntradaEstoque(double quantidadeestoque, double quantidade)
        {
            Quantidade = quantidadeestoque+ quantidade;
            return Quantidade;
        }

        public double SaidaEstoque(double quantidadeestoque, double quantidade)
        {
            Quantidade = quantidadeestoque - quantidade;

            return Quantidade;
        }

        public bool ValidacaoProdutos()
        {
            if (string.IsNullOrEmpty(Descricao))
             {
                throw new ProdutoException("O produto deve conter uma descricao!");
             }
            if (Descricao.Length <3)
            {
                throw new ProdutoException("A descrição do produto deve conter três ou mais letras!");
            }
            if (PrecoUnitario<1)
            {
                throw new ProdutoException("O preço deve ser superior a zero!");
            }
        /*    if (DateTime.Now DataValidade)
            {
                throw new ProdutoException("Data não pode ser Inferior a hoje!");
            }*/

            if (Quantidade!=0)
            {
                throw new ProdutoException(" O Produto deve iniciar com zero estoque!");
            }
            if (Quantidade != 0)
            {
                throw new ProdutoException(" O produto deve iniciar com estoque zero!");
            }
            if (!Ativo)
            {
                throw new ProdutoException("O produto deve inciar Ativo!");
            }

            return true;
        }


    }
}
