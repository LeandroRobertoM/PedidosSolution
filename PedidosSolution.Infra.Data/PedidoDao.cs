using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PedidosSolution.Domain;
using static PedidosSolution.Domain.Pedido;

namespace PedidosSolution.Infra.Data
{
    public class PedidoDao
    {
        private string _connectionString = @"server=.\SQLexpress;initial catalog = CONTROLE_PEDIDO; integrated security = true;";


        public void AdicionarPedido(Pedido novoPedido)
        {
            try
            {
                using (var conexao = new SqlConnection(_connectionString))
                {
                    conexao.Open();

                    using (var comando = new SqlCommand())
                    {
                        string sql;
                        comando.Connection = conexao;

                        if (novoPedido?.Cliente?.IdCliente != null)
                        {

                            sql = @"INSERT Pedido VALUES ( @IDPRODUTO, @IDCLIENTE,@DATA_PEDIDO, @QUANTIDADE, @VALOR_TOTAL,@STATUS);";

                        }
                        else
                        {
                            sql = @"INSERT INTO Pedido (IDPRODUTO,DATA_PEDIDO,QUANTIDADE, VALOR_TOTAL,STATUS) VALUES(@IDPRODUTO,@DATA_PEDIDO, @QUANTIDADE, @VALOR_TOTAL,@STATUS);";
                        }

                        ConverterObjetoParaSql(novoPedido, comando);


                        comando.CommandText = sql;


                        comando.ExecuteNonQuery();
                    }
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<Pedido> BuscaTodos()
        {
            var listaPedido = new List<Pedido>();

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"
                             SELECT P.IDPEDIDO,P.IDPRODUTO,C.IDCLIENTE,C.NOME ,P.DATA_PEDIDO,P.QUANTIDADE,P.STATUS,P.VALOR_TOTAL
                             FROM PEDIDO P
                             LEFT JOIN  CLIENTE C ON P.IDCLIENTE=C.IDCLIENTE 
";


                    comando.CommandText = sql;

                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {

                        Pedido PedidoBuscado = ConverterSqlParaObjeto(leitor);


                        listaPedido.Add(PedidoBuscado);
                    }
                }
            }

            return listaPedido;
        }

        public void DeletarPedido(int pedidoid)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"DELETE FROM Pedido WHERE IDPEDIDO = @IDPEDIDO;";


                    comando.Parameters.AddWithValue("@ID", pedidoid);


                    comando.CommandText = sql;


                    comando.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarPedido(Pedido PedidoBuscado)
        {
            try
            {
                using (var conexao = new SqlConnection(_connectionString))
                {
                    conexao.Open();

                    using (var comando = new SqlCommand())
                    {
                        string sql;
                        comando.Connection = conexao;

                        if (PedidoBuscado?.Cliente?.IdCliente != null)
                        {
                            sql = @"UPDATE PEDIDO SET IDPRODUTO = @IDPRODUTO,IDCLIENTE = @IDCLIENTE,DATA_PEDIDO = @DATA_PEDIDO,QUANTIDADE = @QUANTIDADE,STATUS = @STATUS WHERE IDPEDIDO = @IDPEDIDO;";
                        }
                        else
                        {
                            sql = @"UPDATE Pedido SET IDPRODUTO = @IDPRODUTO,DATA_PEDIDO = @DATA_PEDIDO, QUANTIDADE = @QUANTIDADE,STATUS = @STATUS WHERE IDPEDIDO = @IDPEDIDO;";

                        }

                        ConverterObjetoParaSql(PedidoBuscado, comando);
                        comando.Parameters.AddWithValue("@IDPEDIDO", PedidoBuscado.IdPedido);



                        comando.CommandText = sql;

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AtualizarStatusPedido(int idpedido, Status status)
        {
            try
            {
                using (var conexao = new SqlConnection(_connectionString))
                {
                    conexao.Open();

                    using (var comando = new SqlCommand())
                    {
                        string sql;
                        comando.Connection = conexao;
                        sql = @"UPDATE Pedido SET STATUS = @STATUS WHERE IDPEDIDO = @IDPEDIDO;";
                        comando.Parameters.AddWithValue("@ID", idpedido);
                        comando.Parameters.AddWithValue("@STATUSPEDIDO", status);
                        comando.CommandText = sql;
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void EntradaESaidaEstoque(Pedido PedidoBuscado)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;


                    string sql = @"UPDATE Pedido SET QUANTIDADE_ESTOQUE = @QUANTIDADE_ESTOQUE WHERE IDPEDIDO = @IDPEDIDO;";


                    ConverterObjetoParaSql(PedidoBuscado, comando);
                    comando.Parameters.AddWithValue("@IDPEDIDO", PedidoBuscado.IdPedido);

                    comando.CommandText = sql;


                    comando.ExecuteNonQuery();
                }
            }
        }

        public Pedido BuscaPorTexto(string nome)
        {


            Pedido buscadoPedido = null;

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"SELECT IDPEDIDO, IDPRODUTO, IDCLIENTE, DATA_PEDIDO,
                   QUANTIDADE, VALOR_TOTAL FROM Pedido WHERE DESCRICAO LIKE @TEXTO;";

                    comando.Parameters.AddWithValue("@TEXTO", $"%{nome}%");

                    //ATRIBUIR SCRIPT
                    comando.CommandText = sql;

                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        buscadoPedido = ConverterSqlParaObjeto(leitor);
                    }
                }
            }

            return buscadoPedido;
        }

        public Pedido BuscarPorId(int idPedido)
        {
            var PedidoBuscado = new Pedido();

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"SELECT IDPEDIDO, IDPRODUTO, IDCLIENTE, DATA_PEDIDO,
                   QUANTIDADE, VALOR_TOTAL,STATUS FROM Pedido WHERE IDPEDIDO = @IDPEDIDO;";

                    comando.Parameters.AddWithValue("@IDPEDIDO", idPedido);

                    //ATRIBUIR SCRIPT
                    comando.CommandText = sql;

                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {

                        PedidoBuscado = ConverterSqlParaObjeto(leitor);
                    }
                }
            }

            return PedidoBuscado;
        }

        public Pedido BuscarPorIdStatusPedido(int idPedido)
        {
            var PedidoBuscado = new Pedido();

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"SELECT P.IDPEDIDO,P.IDPRODUTO,C.IDCLIENTE,C.NOME ,P.DATA_PEDIDO,P.QUANTIDADE,P.STATUS,P.VALOR_TOTAL
                             FROM PEDIDO P
                             LEFT JOIN  CLIENTE C ON P.IDCLIENTE=C.IDCLIENTE WHERE IDPEDIDO = @IDPEDIDO;";

                    comando.Parameters.AddWithValue("@IDPEDIDO", idPedido);

                    //ATRIBUIR SCRIPT
                    comando.CommandText = sql;

                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {

                        PedidoBuscado = ConverterSqlParaObjeto(leitor);
                    }
                }
            }

            return PedidoBuscado;
        }

        private Pedido ConverterSqlParaObjeto(SqlDataReader leitor)
        {

            var Pedido = new Pedido();
            Pedido.IdPedido = int.Parse(leitor["IDPEDIDO"].ToString());

            Pedido.Produto = new Produto
            {
                Idproduto = Convert.ToInt32(leitor["IDPRODUTO"].ToString()),
            };

            var idcliente = leitor["IDCLIENTE"].ToString();

            if (!string.IsNullOrWhiteSpace(idcliente))
            {
                Pedido.Cliente = new Cliente
                {
                    Nome = leitor["NOME"].ToString(),
                    IdCliente = Convert.ToInt32(idcliente)
                };

            }

            Pedido.DataPedido = Convert.ToDateTime(leitor["DATA_PEDIDO"].ToString());
            Pedido.QuantidadeProduto = double.Parse(leitor["QUANTIDADE"].ToString());
            Pedido.ValorTotal = double.Parse(leitor["VALOR_TOTAL"].ToString());
            Pedido.status = (Status)int.Parse(leitor["STATUS"].ToString());
            return Pedido;
        }

        private void ConverterObjetoParaSql(Pedido Pedido, SqlCommand comando)
        {

            comando.Parameters.AddWithValue("@IDPEDIDO", Pedido.IdPedido);

            comando.Parameters.AddWithValue("@IDPRODUTO", Pedido.Produto.Idproduto);

            if (Pedido?.Cliente?.IdCliente != null)
            {
                comando.Parameters.AddWithValue("@IDCLIENTE", Pedido.Cliente.IdCliente);
            }
            comando.Parameters.AddWithValue("@DATA_PEDIDO", Pedido.DataPedido);
            comando.Parameters.AddWithValue("@QUANTIDADE", Pedido.QuantidadeProduto);
            comando.Parameters.AddWithValue("@VALOR_TOTAL", Pedido.ValorTotal);
            comando.Parameters.AddWithValue("@STATUS", Pedido.status);
        }

        private void ConverterObjetoUpdateSatusParaSql(Pedido Pedido, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("@IDPEDIDO", Pedido.IdPedido);
            comando.Parameters.AddWithValue("@STATUS", Pedido.status);
        }
    }
}
