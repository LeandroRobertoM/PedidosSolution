using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosSolution.Domain
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public Produto Produto { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime DataPedido { get; set; }
        public double QuantidadeProduto { get; set; }
        public double ValorTotal { get; set; }
        public Status status { get; set; }



        public Pedido(Produto produto, Cliente? cliente, DateTime datapedido, double quantidadeproduto, double valortotal)
        {
            this.Cliente = cliente;
            this.Produto = produto;
            this.DataPedido = datapedido;
            this.QuantidadeProduto = quantidadeproduto;
            this.ValorTotal = valortotal;
        }

        public double CalcularValorPedido(double ValorProduto, double QuantidadeProduto)
        {
            ValorTotal += ValorProduto * QuantidadeProduto;


            return ValorTotal;
        }

        public Pedido() { }


        public bool ValidacaoPedido()
        {
            if (QuantidadeProduto <= 0)
            {
                throw new PedidoException(" O Pedido deve iniciar com valor maior zero estoque!");
            }

            return true;
        }

        public enum Status
        {
            Andamento,
            Transito,
            Finalizado

        }
    }
}
   