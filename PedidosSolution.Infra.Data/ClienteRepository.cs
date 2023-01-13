using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PedidosSolution.Domain;

namespace PedidosSolution.Infra.Data
{
    public class ClienteRepository : IClienteRepository
    {
        static ClienteDao _ClienteDao = new ClienteDao();
        public void CadastrarCliente(Cliente cliente)
        {
            var Cliente = cliente;
            _ClienteDao.AdicionarCliente(Cliente);
        }

        public void EditarCliente(Cliente user)
        {
            var Cliente = user;
            _ClienteDao.AtualizarUsuario(user);
        }

        public List<Cliente> BuscarTodos()
        {
            return _ClienteDao.BuscarCliente();
        }

        public Cliente BuscarPorID(int idCliente)
        {

            return _ClienteDao.BuscarUsuarioPorID(idCliente);

        }

        public Cliente BuscarPorCpf(string cpf)
        {

            return _ClienteDao.BuscarUsuarioPorCPF(cpf);

        }


        public void DeletarCliente(int clienteid)
        {

            _ClienteDao.DeleteConta(clienteid);
        }

        public void AtualizaPontos(Cliente cliente)
        {
            var Cliente = cliente;
            _ClienteDao.AtualizaPontos(cliente);
        }



    }
}
