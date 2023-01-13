using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PedidosSolution.Domain;
using PedidosSolution.Infra.Data;
using ProdutosSolution.Infra.Data;
using static PedidosSolution.Domain.Pedido;

namespace Pedidos.Solution.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IClienteRepository _clienterepository;
        private readonly IProdutoRepository _produtorepository;
        private readonly IPedidoRepository _pedidosrespository;

        static Produto _produto = new Produto();
        static Cliente _cliente = new Cliente();
        static Pedido _pedido =  new Pedido();

        public PedidoController()
        {
            _clienterepository = new ClienteRepository();
            _produtorepository = new ProdutoRepository();
            _pedidosrespository = new PedidoRepository();

        }

        [HttpPost]
        public IActionResult Pedidos(Pedido novoPedido)
        {

            if (novoPedido.ValidacaoPedido())
            {
                try
                {
                    var ProdutoBuscado = _produtorepository.BuscarPorID(novoPedido.Produto.Idproduto);
                     novoPedido.ValorTotal = _pedido.CalcularValorPedido(ProdutoBuscado.PrecoUnitario,novoPedido.QuantidadeProduto);
                    if (novoPedido.Cliente?.Cpf != null)
                    {
                        var ClienteBuscado = _clienterepository.BuscarPorCpf(Convert.ToString(novoPedido.Cliente.Cpf));
                        if (ClienteBuscado?.IdCliente != null)
                        {
                            novoPedido.Cliente.IdCliente = ClienteBuscado.IdCliente;
                        }
                    }
                    _pedidosrespository.CadastrarPedido(novoPedido);
                    _produtorepository.EntradaESaidaEstoque(ProdutoBuscado);

                    if (novoPedido.Cliente?.Cpf != null)
                    {
                        
                      var ClienteBuscado = _clienterepository.BuscarPorCpf(Convert.ToString(novoPedido.Cliente.Cpf));
                        ClienteBuscado.PontosFidelidade = _cliente.CalcularPontos(novoPedido.ValorTotal);
                        _clienterepository.AtualizaPontos(ClienteBuscado);
                    }

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
        public IActionResult PutPedido(Pedido PedidoEditado)
        {
 
            try
            {
                _pedidosrespository.EditarPedido(PedidoEditado);

                return Ok(new Resposta(200, "Editado com sucesso!"));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }


        [HttpGet]
        public IActionResult GetPedidos()
        {
            try
            {
                return Ok(_pedidosrespository.BuscarTodos());
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpGet("{idPedido}")]
        public IActionResult GetPedidoId(int idPedido)
        {

            var clienteBuscado = _pedidosrespository.BuscarPorIDStatus(idPedido);

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


        [HttpPatch, Route("​manager/atualizastatus")]
        public IActionResult PutAtualizaStatus(int pedido, Status status)
        {

            try
            {
                _pedidosrespository.AtualizaStatus(pedido, status);

                return Ok(new Resposta(200, "Editado com sucesso!"));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }

        }



    }


}
