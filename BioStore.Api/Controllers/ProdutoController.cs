using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BioStore.Domain.StoreContext.Commands.MarcaCommands.Inputs;
using BioStore.Domain.StoreContext.Handlers;
using BioStore.Domain.StoreContext.Queries;
using BioStore.Domain.StoreContext.Repositories;
using BioStore.Shared.Commands;
using BioStore.Shared.Commands.Output;
using Microsoft.AspNetCore.Mvc;

namespace BioStore.Api.Controllers
{
    [Route("v1/Produto")]
    public class ProdutoController : Controller
    {
        private readonly ProdutoHandler _handler;        
        private readonly IProdutoRepository _produtoRepositoty;

        public ProdutoController(IProdutoRepository marcaRepository, ProdutoHandler handler)
            => (_produtoRepositoty, _handler) = (marcaRepository, handler);

        [HttpGet("ObterProdutos")]
        public IList<ListaDeProdutoResult> ObterProdutos()
        {
            return _produtoRepositoty.ObterTodos();
        }

        [HttpGet("Disponibilidade")]
        public IList<ListaDeDisponibilidadeResult> Get()
        {
            return _produtoRepositoty.Disponibilidade();
        }

        [HttpGet("codigo/{codigo:long}")]
        public bool Get(long codigo)
        {
            return _produtoRepositoty.CodigoExiste(codigo);
        }


        [HttpPost]
        public ICommandResult Post([FromBody] CriarProdutoCommand produto)
        {
            if (produto.IsValid)
            {
                var result = _handler.Handle(produto);
                return result;
            }

            return new CommandResult(false, "Erro ao cadastrar produto", new
            {
                produto.Notifications
            });
        }
    }
}