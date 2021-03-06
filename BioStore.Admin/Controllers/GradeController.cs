using BioStore.Admin.Helper;
using BioStore.Admin.Models;
using BioStore.Admin.Queries;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using X.PagedList;

namespace BioStore.Admin.Controllers
{
    public class GradeController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public GradeController(IHostingEnvironment hostingEnvironment)
            => (_hostingEnvironment) = (hostingEnvironment);


        [HttpGet]
        public IActionResult Index(int? page = 1)
        {
            //var Grades = GetApiMethod<IList<ListaDeGradeResult>>("v1/Grade", null);
            //return View(Grades);


            var GradeResult = new GradeResult();

            if (page < 0)
            {
                page = 1;
            }

            var pageIndex = (page ?? 1) - 1;
            var pageSize = 5;

            int totalProductCount = GetApiMethod<IList<ListaDeGradeResult>>("v1/Grade", null).Count;
            var Grades = GetApiMethod<IList<ListaDeGradeResult>>($"v1/Grade/{page}/{pageSize}", null);
            var GradePagedList = new StaticPagedList<ListaDeGradeResult>(Grades, pageIndex + 1, pageSize, totalProductCount);
            GradeResult.Grades = GradePagedList;
            GradeResult.pageSize = page;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Grades", GradeResult);
            }

            return View(GradeResult);

        }

        [HttpGet("Grade/Editar/{id:guid}")]
        public IActionResult Editar(Guid id)
        {
            var grade = GetApiMethod<GradeViewModel>($"v1/Grade/{id}", null);
            return View("Cadastrar", grade);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(GradeViewModel grade)
        {   
            var result = PostApiMethodRsult("v1/Grade", grade);
            if (result.Success)
            {
                var gradeResult = JsonConvert.DeserializeObject<GradeViewModel>(result.Data.ToString());
                return View("Variacao", new VariacaoViewModel { Grade = gradeResult });

            }
            else
                return View(grade);           
        }

        [HttpPost]
        public IActionResult Atualizar(GradeViewModel grade)
        {     
            var result = PutApiMethodResult($"v1/grade/{grade.GradeId}", grade);

            if (result.Success)
                return RedirectToAction("Index");
            else
                return View("Cadastrar", grade);
        }
     
    }
}