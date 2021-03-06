using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BioStore.Domain.StoreContext.Commands.GradeCommands.Inputs;
using BioStore.Domain.StoreContext.Enums;
using BioStore.Domain.StoreContext.Handlers;
using BioStore.Domain.StoreContext.Queries;
using BioStore.Domain.StoreContext.Repositories;
using BioStore.Shared.Commands;
using BioStore.Shared.Commands.Output;
using Microsoft.AspNetCore.Mvc;

namespace BioStore.Api.Controllers
{
    [Route("v1/Grade")]
    public class GradeController : Controller
    {
        private readonly GradeHandler _handler;
        private readonly IGradeRepository _GradeRepositoty;

        public GradeController(GradeHandler handler, IGradeRepository GradeRepository)
            => (_handler, _GradeRepositoty) = (handler, GradeRepository);

        [HttpGet("{id:guid}/variacao")]
        public IList<ListaDeVariacaoResult> variacao(Guid id)
        {
            return _GradeRepositoty.ObterVariacaoPorGrade(id);
        }

        [HttpPost("{id:guid}/variacao")]
        public ICommandResult variacao([FromBody] CriarVariacaoCommand variacao)
        {

            if (variacao.IsValid)
            {   
                var result = _handler.Handle(variacao);
                return result;
            }

            return new CommandResult(false, "Erro ao cadastrar Variação", new
            {
                variacao.Notifications
            });            
        }

        [HttpGet]
        public IList<ListaDeGradeResult> Get()
        {
            return _GradeRepositoty.ObterTodos();
        }

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public IList<ListaDeGradeResult> Get(int pageNumber, int pageSize)
        {
            return _GradeRepositoty.ObterPaginacao(pageNumber,pageSize);
        }

        [HttpGet("{id:guid}")]
        public GradeResult Get(Guid id)
        {
            return _GradeRepositoty.ObterPorId(id);
        }

        [HttpPost]        
        public ICommandResult Post([FromBody] CriarGradeCommand Grade)
        {
            if (Grade.IsValid)
            {
                var result = _handler.Handle(Grade);                
                return result;                
            }
           
            return new CommandResult(false, "Erro ao cadastrar Grade", new
            {
                Grade.Notifications              
            });
        }

        [HttpPut("{id:guid}")]
        public ICommandResult Put([FromBody] CriarGradeCommand Grade, Guid id)
        {
            if (Grade.IsValid)
            {
                var result = _handler.Handle(Grade);
                return result;
            }

            return new CommandResult(false, "Erro ao atualizar Grade", new
            {
                Grade.Notifications
            });
        }

        [HttpDelete("Variacao/{id:guid}")]
        public ICommandResult Delete(Guid id)
        {

            int total = _GradeRepositoty.ExcluirVariacao(id);
            if(total > 0)
            {
                return new CommandResult(true, "Variação excluída com sucesso", new
                {
                    VariacaoId = id
                }, Shared.Enums.EtipoMensagem.Info, false);
            }
            else
            {
                return new CommandResult(false, "Erro ao excluir variação", new
                {
                    VariacaoId = id
                }, Shared.Enums.EtipoMensagem.Error, false);
            }
            
        }
    }
}