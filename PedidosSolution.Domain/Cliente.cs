using System;

namespace PedidosSolution.Domain
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public long Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public double PontosFidelidade { get; set; }

        public Cliente() { }

        public Cliente(string nome, long cpf, DateTime datanascimento, double pontosfidelidade)
        {
            this.Nome = nome;
            this.Cpf = cpf;
            this.DataNascimento = datanascimento;
            this.PontosFidelidade = pontosfidelidade;
        }

        public double CalcularPontos(double ValorTotal)
        {
            PontosFidelidade += ValorTotal * 2;
            return PontosFidelidade;
        }

        public bool ValidacaoClientes()
        {
            if (string.IsNullOrEmpty(Nome))
            {
                throw new ProdutoException("O Cliente deve conter um nome!");
            }
            if (Nome.Length < 3)
            {
                throw new ProdutoException("O cliente deve conter no minimo 3 letras!");
            }
 
            return true;
        }



    }
}
