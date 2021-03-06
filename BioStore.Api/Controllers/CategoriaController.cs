using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BioStore.Domain.StoreContext.Commands.CategoriaCommands.Inputs;
using BioStore.Domain.StoreContext.Commands.MarcaCommands.Inputs;
using BioStore.Domain.StoreContext.Enums;
using BioStore.Domain.StoreContext.Handlers;
using BioStore.Domain.StoreContext.Queries;
using BioStore.Domain.StoreContext.Repositories;
using BioStore.Shared.Commands;
using BioStore.Shared.Commands.Output;
using Microsoft.AspNetCore.Mvc;

namespace BioStore.Api.Controllers
{
    [Route("v1/Categoria")]
    public class CategoriaController : Controller
    {
        private readonly CategoriaHandler _handler;
        private readonly ICategoriaRepository _categoriaRepositoty;

        public CategoriaController(CategoriaHandler handler, ICategoriaRepository categoriaRepository)
            => (_handler, _categoriaRepositoty) = (handler, categoriaRepository);

        [HttpGet]
        public IList<ListaDeCategoriaResult> Get()
        {
            return _categoriaRepositoty.ObterTodos();
        }

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public IList<ListaDeCategoriaResult> Get(int pageNumber, int pageSize)
        {
            return _categoriaRepositoty.ObterPaginacao(pageNumber,pageSize);
        }

        [HttpGet("{id:guid}")]
        public CategoriaResult Get(Guid id)
        {
            return _categoriaRepositoty.ObterPorId(id);
        }

        [HttpGet("ObterPorNone/{termo}")]
        public IList<ListaDeCategoriaResult> ObterPorNone(string termo)
        {
            return _categoriaRepositoty.ObterPorNone(termo);
        }

        [HttpPost]        
        public ICommandResult Post([FromBody] CriarCategoriaCommand categoria)
        {
            if (categoria.IsValid)
            {
                var result = _handler.Handle(categoria);                
                return result;                
            }
           
            return new CommandResult(false, "Erro ao cadastrar categoria", new
            {
                categoria.Notifications              
            });
        }

        [HttpPut("{id:guid}")]
        public ICommandResult Put([FromBody] CriarCategoriaCommand categoria, Guid id)
        {
            if (categoria.IsValid)
            {
                var result = _handler.Handle(categoria);
                return result;
            }

            return new CommandResult(false, "Erro ao atualizar categoria", new
            {
                categoria.Notifications
            });
        }

        [HttpPut("{status:int}/{id:guid}")]
        public ICommandResult Put(ECategoriaStatus status, Guid id)
        {
            var result = _handler.Handle(status, id);
            return result;           
        }
    }
}