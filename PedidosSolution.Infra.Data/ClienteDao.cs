using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using PedidosSolution.Domain;

namespace PedidosSolution.Infra.Data
{
    public class ClienteDao
    {
       // private string _connectionString = @"server=DESKTOP-RT242MK\SQLEXPRESS;initial catalog = CONTROLE_PEDIDO; integrated security = true;";
        private string _connectionString = @"server=.\SQLexpress;initial catalog = CONTROLE_PEDIDO; integrated security = true;";

        public void AdicionarCliente(Cliente novoCliente)
        {
            try
            {

                using (var conexao = new SqlConnection(_connectionString))
                {
                    conexao.Open();

                    using (var comando = new SqlCommand())
                    {
                        comando.Connection = conexao;
                        string sql = @"INSERT CLIENTE VALUES(@NOME,@CPF,@DATA_NASCIMENTO,@FIDELIDADE);";
                        ConverterObjetoParaSql(novoCliente, comando);
                        comando.CommandText = sql;
                        comando.ExecuteNonQuery();
                    }

                    Console.WriteLine($"Cliente {novoCliente.Nome} incluido com sucesso");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AtualizaPontos(Cliente cliente)
        {
          
                using (var conexao = new SqlConnection(_connectionString))
                {
                    conexao.Open(); //ABRIR CONEXÃO

                    using (var comando = new SqlCommand())
                    {
                        comando.Connection = conexao; //CRIAR UM COMANDO

                        //CRIA SCRIPT
                        string sql = @"UPDATE CLIENTE SET FIDELIDADE = @FIDELIDADE WHERE IDCLIENTE = @IDCLIENTE;";

                        //ADICIONAR PARAMETROS
                        ConverterObjetoParaSql(cliente, comando);
                        comando.Parameters.AddWithValue("@IDCLIENTE", cliente.IdCliente);

                        //ATRIBUIR SCRIPT
                        comando.CommandText = sql;

                        //EXECUTAR SCRIPT
                        comando.ExecuteNonQuery();
                    }
                }
         }
        

        public void AtualizarUsuario(Cliente ClienteEditado)
        {

            try
            {
                using (var conexao = new SqlConnection(_connectionString))
                {
                    conexao.Open();

                    using (var comando = new SqlCommand())
                    {
                        comando.Connection = conexao;

                        string sql = @"UPDATE CLIENTE SET  NOME = @NOME, CPF = @CPF,DATA_NASCIMENTO= @DATA_NASCIMENTO, FIDELIDADE = @FIDELIDADE WHERE IDCLIENTE = @IDCLIENTE ";

                        comando.Parameters.AddWithValue("@IDCLIENTE", ClienteEditado.IdCliente);


                        ConverterObjetoParaSql(ClienteEditado, comando);

                        comando.CommandText = sql;

                        comando.ExecuteNonQuery();
                    }
                }

                Console.WriteLine($"Cliente {ClienteEditado.IdCliente} incluido com sucesso");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public List<Cliente> BuscarCliente()
        {
            var listaClientes = new List<Cliente>();

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT IDCLIENTE,NOME,CPF,DATA_NASCIMENTO,FIDELIDADE FROM CLIENTE";
                    comando.CommandText = sql;
                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        Cliente Clientebuscado = ConverterSqlParaObjeto(leitor);

                        listaClientes.Add(Clientebuscado);
                    }
                }


            }

            return listaClientes;
        }

        public Cliente BuscarUsuarioPorID(int id)
        {
            Cliente ClienteBuscada = null;

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"SELECT IDCLIENTE,NOME,CPF,DATA_NASCIMENTO,FIDELIDADE FROM Cliente WHERE IDCLIENTE = @IDCLIENTE";

                    comando.Parameters.AddWithValue("@IDCLIENTE", id);

                    comando.CommandText = sql;

                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        ClienteBuscada = ConverterSqlParaObjeto(leitor);
                    }
                }
            }

            return ClienteBuscada;
        }

        public Cliente BuscarUsuarioPorCPF(string cpf)
        {
            Cliente ClienteBuscada = null;

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();


                using (var comando = new SqlCommand())
                {

                    try
                    {

                        comando.Connection = conexao;


                        string sql = @"SELECT IDCLIENTE,NOME,CPF,DATA_NASCIMENTO,FIDELIDADE FROM Cliente WHERE CPF LIKE @TEXTO";

                        comando.Parameters.AddWithValue("@TEXTO", $"%{cpf}%");

                        comando.CommandText = sql;

                        SqlDataReader leitor = comando.ExecuteReader();

                        while (leitor.Read())
                        {
                            ClienteBuscada = ConverterSqlParaObjeto(leitor);
                        }

                      

                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }


                }

            }

            return ClienteBuscada;
        }

        public void DeleteConta(int clienteid)
        {

            using (var conexao = new SqlConnection(_connectionString))
            {
                try
                {
                    conexao.Open();

                    using (var comando = new SqlCommand())
                    {
                        comando.Connection = conexao;

                        string sql = @"DELETE FROM CLIENTE WHERE IDCLIENTE = @IDCLIENTE";

                        comando.Parameters.AddWithValue("@IDCLIENTE", clienteid);

                        comando.CommandText = sql;

                        comando.ExecuteNonQuery();
                    }
                }

                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void ConverterObjetoParaSql(Cliente cliente, SqlCommand comando)
        {

            comando.Parameters.AddWithValue("@NOME", cliente.Nome);
            comando.Parameters.AddWithValue("@CPF", cliente.Cpf);
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", cliente.DataNascimento);
            comando.Parameters.AddWithValue("@FIDELIDADE", cliente.PontosFidelidade);

        }
        private Cliente ConverterSqlParaObjeto(SqlDataReader leitor)
        {
            var Cliente = new Cliente();
            Cliente.IdCliente = Convert.ToInt32(leitor["IDCLIENTE"].ToString());
            Cliente.Nome = leitor["NOME"].ToString();
            Cliente.Cpf = long.Parse(leitor["CPF"].ToString());
            Cliente.DataNascimento = Convert.ToDateTime(leitor["DATA_NASCIMENTO"].ToString());
            Cliente.PontosFidelidade = Convert.ToInt32(leitor["FIDELIDADE"].ToString());
            return Cliente;
        }
    }
}
