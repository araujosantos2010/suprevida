using BioStore.Admin.Models;
using BioStore.Admin.Queries;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BioStore.Admin.Controllers
{
    public class VariacaoController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public VariacaoController(IHostingEnvironment hostingEnvironment)
            => (_hostingEnvironment) = (hostingEnvironment);
        
        [HttpGet("Variacao/{id:guid}")]
        public IActionResult Index(Guid id)
        {
            var variacaoResult = GetApiMethod<IList<ListaDeVariacaoResult>>($"v1/Grade/{id}/Variacao", null);
            var grade = GetApiMethod<GradeViewModel>($"v1/Grade/{id}", null);
            ViewBag.Variacoes = variacaoResult;
            var variacao = new VariacaoViewModel { Grade = grade };
            return View(variacao);

        }

        [HttpPost("{gradeId:guid}/Variacao/{id:guid}/Excluir")]
        [IgnoreAntiforgeryToken]
        public IActionResult Excluir(Guid id, Guid gradeId)
        {
            DeleteApiMethod($"v1/Grade/Variacao/{id}", null);
            var variacaoResult = GetApiMethod<IList<ListaDeVariacaoResult>>($"v1/Grade/{gradeId}/Variacao", null);            
            ViewBag.Variacoes = variacaoResult;            
            return PartialView("_variacoes");

        }

        [HttpPost]
        public IActionResult Cadastrar(VariacaoViewModel variacao)
        {            
            var result = PostApiMethodRsult($"v1/Grade/{variacao.Grade.GradeId}/Variacao", variacao);               
            var variacaoResult = GetApiMethod<IList<ListaDeVariacaoResult>>($"v1/Grade/{variacao.Grade.GradeId}/Variacao", null);
            ViewBag.Variacoes = variacaoResult;
            return PartialView("_variacoes");                
        }
    }
}