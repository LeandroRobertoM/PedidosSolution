using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PedidosSolution.Domain;

namespace PedidosSolution.Infra.Data
{
    public class ProdutoDao
    {
        private string _connectionString = @"server=.\SQLexpress;initial catalog = CONTROLE_PEDIDO; integrated security = true;";

        public ProdutoDao() { }

        public void AdicionarProduto(Produto novoProduto)
        {
            string mensagem = "";

        

                using (var conexao = new SqlConnection(_connectionString))
                {
                    conexao.Open();

                    using (var comando = new SqlCommand())
                    {
                        comando.Connection = conexao;
                        string sql = @"INSERT PRODUTO VALUES(@DESCRICAO,@PRECO_UNITARIO,@DATA_VALIDADE,@QUANTIDADE,@ATIVO);";
                        ConverterObjetoParaSql(novoProduto, comando);
                        comando.CommandText = sql;
                        comando.ExecuteNonQuery();
                    }

                }
            }



          

        public List<Produto> BuscarProdutos()
        {
            var listaProdutos = new List<Produto>();

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT * FROM PRODUTO";
                    comando.CommandText = sql;
                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        Produto produtobuscado = ConverterSqlParaObjeto(leitor);

                        listaProdutos.Add(produtobuscado);
                    }
                }


            }

            return listaProdutos;
        }


        public List<Produto> BuscarProdutosAtivos()
        {
            var listaProdutos = new List<Produto>();

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT * FROM PRODUTO Where ATIVO=1;";
                    comando.CommandText = sql;
                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        Produto produtobuscado = ConverterSqlParaObjeto(leitor);

                        listaProdutos.Add(produtobuscado);
                    }
                }


            }

            return listaProdutos;
        }

        public void AtualizarProduto(Produto ProdutoEditado)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"UPDATE PRODUTO SET DESCRICAO = @DESCRICAO, DATA_VALIDADE = @DATA_VALIDADE,PRECO_UNITARIO = @PRECO_UNITARIO, QUANTIDADE = @QUANTIDADE,ATIVO = @ATIVO WHERE IDPRODUTO = @IDPRODUTO";

                    comando.Parameters.AddWithValue("@IDPRODUTO", ProdutoEditado.Idproduto);

                    ConverterObjetoParaSql(ProdutoEditado, comando);

                    comando.CommandText = sql;

                    comando.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProduto(int produto)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"DELETE FROM PRODUTO WHERE IDPRODUTO = @IDPRODUTO";

                    comando.Parameters.AddWithValue("@IDPRODUTO", produto);

                    comando.CommandText = sql;

                    comando.ExecuteNonQuery();
                }
            }
        }

        public Produto BuscarprodutoPorID(int id)
        {
            Produto produtoBuscado = null;

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"SELECT IDPRODUTO,DESCRICAO,DATA_VALIDADE,PRECO_UNITARIO,QUANTIDADE,ATIVO FROM PRODUTO WHERE IDPRODUTO = @IDPRODUTO";

                    comando.Parameters.AddWithValue("@IDPRODUTO", id);

                    comando.CommandText = sql;

                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        produtoBuscado = ConverterSqlParaObjeto(leitor);
                    }
                }
            }

            return produtoBuscado;
        }

        public Produto BuscarProdutoPorNome(string nome)
        {
            Produto produtoBuscado = null;

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"SELECT IDPRODUTO,DESCRICAO,DATA_VALIDADE,PRECO_UNITARIO,QUANTIDADE,ATIVO FROM PRODUTO WHERE IDPRODUTO LIKE @DESCRICAO";

                    comando.Parameters.AddWithValue("@TEXTO", $"%{nome}%");

                    comando.CommandText = sql;

                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        produtoBuscado = ConverterSqlParaObjeto(leitor);
                    }
                }
            }

            return produtoBuscado;
        }

        public void EntradaESaidaEstoque(Produto produtoBuscado)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open(); //ABRIR CONEXÃO

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao; //CRIAR UM COMANDO

                    //CRIA SCRIPT
                    string sql = @"UPDATE PRODUTO SET QUANTIDADE = @QUANTIDADE WHERE IDPRODUTO = @IDPRODUTO;";

                    //ADICIONAR PARAMETROS
                    ConverterObjetoParaSql(produtoBuscado, comando);
                    comando.Parameters.AddWithValue("@IDPRODUTO", produtoBuscado.Idproduto);

                    //ATRIBUIR SCRIPT
                    comando.CommandText = sql;

                    //EXECUTAR SCRIPT
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void DesativaProduto(Produto produtoBuscado)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open(); //ABRIR CONEXÃO

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao; //CRIAR UM COMANDO

                    //CRIA SCRIPT
                    string sql = @"UPDATE PRODUTO SET ATIVO = @ATIVO WHERE IDPRODUTO = @IDPRODUTO;";

                    //ADICIONAR PARAMETROS
                    ConverterObjetoParaSql(produtoBuscado, comando);
                    comando.Parameters.AddWithValue("@IDPRODUTO", produtoBuscado.Idproduto);
                    comando.Parameters.AddWithValue("@ATIVO", produtoBuscado.Ativo);

                    //ATRIBUIR SCRIPT
                    comando.CommandText = sql;

                    //EXECUTAR SCRIPT
                    comando.ExecuteNonQuery();
                }
            }
        }

        



        private void ConverterObjetoParaSql(Produto produto, SqlCommand comando)
        {

   
            comando.Parameters.AddWithValue("@DESCRICAO", produto.Descricao);
            comando.Parameters.AddWithValue("@PRECO_UNITARIO", produto.PrecoUnitario);
            comando.Parameters.AddWithValue("@DATA_VALIDADE", produto.DataValidade);
        
            comando.Parameters.AddWithValue("@QUANTIDADE", produto.Quantidade);


        }

        private Produto ConverterSqlParaObjeto(SqlDataReader leitor)
        {
            Produto produtoBuscado = new Produto();
            produtoBuscado.Idproduto = int.Parse(leitor["IDPRODUTO"].ToString());
           
            produtoBuscado.Descricao = leitor["DESCRICAO"].ToString();
            produtoBuscado.DataValidade = Convert.ToDateTime(leitor["DATA_VALIDADE"].ToString());
            produtoBuscado.PrecoUnitario = double.Parse(leitor["PRECO_UNITARIO"].ToString());
            produtoBuscado.Quantidade = double.Parse(leitor["QUANTIDADE"].ToString());
            produtoBuscado.Ativo = Boolean.Parse((leitor["ATIVO"].ToString()));

            return produtoBuscado;


        }

    }
}


