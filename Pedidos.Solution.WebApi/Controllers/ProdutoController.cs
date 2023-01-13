using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PedidosSolution.Domain;
using ProdutosSolution.Infra.Data;

namespace Pedidos.Solution.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _repository;
        static Produto _Produto = new Produto();

        public ProdutoController()
        {
            _repository = new ProdutoRepository();
           
        }
        [HttpPost]
        public IActionResult PostProduto(Produto novoProduto)
        {
            var validacao = novoProduto.ValidacaoProdutos();
            if (validacao == true)
            {

                try
                {
                    _repository.CadastrarProduto(novoProduto);

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
        public IActionResult PutProduto(Produto protudoEditado)
        
        {
          
            try
            {
                _repository.EditarProduto(protudoEditado);

                return Ok(new Resposta(200, "Editado com sucesso!"));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        
        }

        [HttpPut, Route("​manager/entradaestoque")]
        public IActionResult PutAtualizarEstoque(Produto protudoEntrada)
        {
            var ProdutoBuscado = _repository.BuscarPorID(protudoEntrada.Idproduto);

            protudoEntrada.Quantidade = _Produto.EntradaEstoque(ProdutoBuscado.Quantidade, protudoEntrada.Quantidade);

            try
            {  
                _repository.EntradaESaidaEstoque(protudoEntrada);

                return Ok(new Resposta(200, "Editado com sucesso!"));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }

        }

        [HttpPatch, Route("​manager/desativa")]
        public IActionResult PutDesativaProduto([FromBody] Produto produtoEntrada)
        {
         
            try
            {
                _repository.DesativaProduto(produtoEntrada);

                return Ok(new Resposta(200, "Editado com sucesso!"));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }

        }

        [HttpGet]
        public IActionResult GetProdutos()
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


        [HttpGet,Route("buscaativo")]
        public IActionResult GetProdutosAtivo()
        {
            try
            {
                return Ok(_repository.BuscarProdutosAtivos());
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }


        [HttpGet("{IdProduto}")]
        public IActionResult GetProdutoId(int IdProduto)
        {

            var ProdutoBuscado = _repository.BuscarPorID(IdProduto);

            try
            {


                if (ProdutoBuscado == null)
                    return BadRequest("Nenhum registro encontrado!");

            }

            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }

            return Ok(ProdutoBuscado);



        }



        [HttpDelete("{IdProduto}")]
        public IActionResult DeleteProdutos(int Idproduto)
        {
            var produtoBuscado = _repository.BuscarPorID(Idproduto);

            if (produtoBuscado == null)
                return BadRequest("Nenhum registro encontrado!");

            try
            {
                _repository.DeletarProduto(Idproduto);
            }

            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }

            return Ok(new Resposta(200, "Excluido com Sucesso com sucesso!"));
        }

    }
}

