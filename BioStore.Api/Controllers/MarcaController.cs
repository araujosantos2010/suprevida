using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [Route("v1/Marca")]
    public class MarcaController : Controller
    {
        private readonly MarcaHandler _handler;
        private readonly IMarcaRepository _marcaRepositoty;

        public MarcaController(MarcaHandler handler, IMarcaRepository marcaRepository)
            => (_handler, _marcaRepositoty) = (handler, marcaRepository);

        [HttpGet]
        public IList<ListaDeMarcaResult> Get()
        {
            return _marcaRepositoty.ObterTodos();
        }

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public IList<ListaDeMarcaResult> Get(int pageNumber, int pageSize)
        {
            return _marcaRepositoty.ObterPaginacao(pageNumber,pageSize);
        }

        [HttpGet("{id:guid}")]
        public MarcaResult Get(Guid id)
        {
            return _marcaRepositoty.ObterPorId(id);
        }

        [HttpPost]        
        public ICommandResult Post([FromBody] CriarMarcaCommand marca)
        {
            if (marca.IsValid)
            {
                var result = _handler.Handle(marca);                
                return result;                
            }
           
            return new CommandResult(false, "Erro ao cadastrar marca", new
            {
                marca.Notifications              
            });
        }

        [HttpPut("{id:guid}")]
        public ICommandResult Put([FromBody] CriarMarcaCommand marca, Guid id)
        {
            if (marca.IsValid)
            {
                var result = _handler.Handle(marca);
                return result;
            }

            return new CommandResult(false, "Erro ao atualizar marca", new
            {
                marca.Notifications
            });
        }

        [HttpPut("{status:int}/{id:guid}")]
        public ICommandResult Put(EMarcaStatus status, Guid id)
        {
            var result = _handler.Handle(status, id);
            return result;           
        }
    }
}