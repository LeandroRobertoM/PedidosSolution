using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PedidosSolution.Domain;
using PedidosSolution.Infra.Data;

namespace Pedidos.Solution.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _repository;

        public ClienteController()
        {
            _repository = new ClienteRepository();

        }
        [HttpPost]
        public IActionResult PostCliente(Cliente novoCliente)
        {
            var validacao = novoCliente.ValidacaoClientes();
            if (validacao == true)
            {

                try
                {
                    _repository.CadastrarCliente(novoCliente);

                    return Ok(new Resposta(200, "Criado com sucesso!"));
                }
                catch (Exception e)
                {
                    return StatusCode(500, new Resposta(500, e.Message));
                }
            }
            else
            {
                return StatusCode(500, "Ocorreu um erro na validação");
            }

        }

        [HttpPut]
        public IActionResult PutCliente(Cliente ClienteEditado)
        {
            
            var clienteBuscado = _repository.BuscarPorID(ClienteEditado.IdCliente);



            if (clienteBuscado == null)
                return BadRequest("Nenhum registro encontrado!");
            var validacao = ClienteEditado.ValidacaoClientes();
            if (validacao == true)
            {


                try
                {
                    _repository.CadastrarCliente(ClienteEditado);

                    return Ok(new Resposta(200, "Criado com sucesso!"));
                }
                catch (Exception e)
                {
                    return StatusCode(500, new Resposta(500, e.Message));
                }
            }
            else
            {
                return StatusCode(500, "Ocorreu um erro na validação");
            }

        }

        [HttpGet]
        public IActionResult GetClientes()
        {
            try
            {
                return Ok(_repository.BuscarTodos());
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }


        [HttpGet("{IdCliente}")]
        public IActionResult GetClienteId(int IdCliente)
        {

            var clienteBuscado = _repository.BuscarPorID(IdCliente);

            try
            {


                if (clienteBuscado == null)
                    return BadRequest("Nenhum registro encontrado!");

            }

            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }

            return Ok(clienteBuscado);

        }

        [HttpGet, Route("api/Cliente/PesquisaPorCPF")]
        public IActionResult GetPorCpf(string cpf)
        {

            var clienteBuscado = _repository.BuscarPorCpf(cpf);

            try
            {


                if (clienteBuscado == null)
                    return BadRequest("Nenhum registro encontrado!");

            }

            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }

            return Ok(clienteBuscado);



        }


        [HttpDelete("{IdCliente}")]
        public IActionResult DeletePublicaca(int IdCliente)
        {

            try
            {
                _repository.DeletarCliente(IdCliente);
            }

            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }

            return Ok(new Resposta(200, "Editado com sucesso!"));
        }

    }
}


