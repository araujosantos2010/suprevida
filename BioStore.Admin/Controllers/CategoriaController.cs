using BioStore.Admin.Helper;
using BioStore.Admin.Models;
using BioStore.Admin.Queries;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace BioStore.Admin.Controllers
{

    public class CategoriaController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public CategoriaController(IHostingEnvironment hostingEnvironment)
            => (_hostingEnvironment) = (hostingEnvironment);


        [HttpGet]
        public IActionResult Index(int? page = 1)
        {
            //var marcas = GetApiMethod<IList<ListaDeMarcaResult>>("v1/Marca", null);
            //return View(marcas);

            var categoriaResult = new CategoriaResult();

            if (page < 0)
            {
                page = 1;
            }

            var pageIndex = (page ?? 1) - 1;
            var pageSize = 10;

            int totalProductCount = GetApiMethod<IList<ListaDeCategoriaResult>>("v1/Categoria", null).Count;
            var categorias = GetApiMethod<IList<ListaDeCategoriaResult>>($"v1/Categoria/{page}/{pageSize}", null);
            var categoriaPagedList = new StaticPagedList<ListaDeCategoriaResult>(categorias, pageIndex + 1, pageSize, totalProductCount);
            categoriaResult.Categorias = categoriaPagedList;
            categoriaResult.pageSize = page;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_categorias", categoriaResult);
            }

            return View(categoriaResult);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            var TodasCategorias = GetApiMethod<IList<CategoriaViewModel>>($"v1/Categoria", null);

            var data = new List<SelectListItem>
            {
                new SelectListItem { Text = "Selecione", Value = "" }
            };

            foreach (var item in TodasCategorias)
            {
                data.Add(new SelectListItem { Text = item.Nome, Value = item.CategoriaId.ToString() });
            }

            ViewBag.TodasCategorias = data;

            return View();
        }

        public IActionResult Filtro(string term)
        {
            var categorias = GetApiMethod<IList<CategoriaViewModel>>($"v1/Categoria/ObterPorNone/{term}", null);

            return Json(categorias.Where(x => x.Nome.StartsWith(term, StringComparison.CurrentCultureIgnoreCase)).ToArray());
        }

        [HttpGet("Categoria/Editar/{id:guid}")]
        public IActionResult Editar(Guid id)
        {
            var categoria = GetApiMethod<CategoriaViewModel>($"v1/Categoria/{id}", null);

            var TodasCategorias = GetApiMethod<IList<CategoriaViewModel>>($"v1/Categoria", null);

            var data = new List<SelectListItem>
            {
                new SelectListItem { Text = "Selecione", Value = "" }
            };

           var _todasCategorias = TodasCategorias.Where(c => c.CategoriaId != categoria.CategoriaId);

            foreach (var item in _todasCategorias)
            {
                data.Add(new SelectListItem { Text = item.Nome, Value = item.CategoriaId.ToString() });
            }

            if(categoria.CategoriaPaiId != default(Guid))
            {
                categoria.CategoriaPai = _todasCategorias.Single(c => c.CategoriaId == categoria.CategoriaPaiId);
            }

            ViewBag.TodasCategorias = data;


            return View("Cadastrar", categoria);
        }

        [HttpPost]
        public IActionResult Cadastrar(CategoriaViewModel categoria)
        {           
            var result = PostApiMethodRsult("v1/Categoria", categoria);
            if (result.Success)
                return RedirectToAction("Index");
            else
                return View(categoria);           
        }

        [HttpPost]
        public IActionResult Atualizar(CategoriaViewModel categoria)
        {            
            
            var result = PutApiMethodResult($"v1/Categoria/{categoria.CategoriaId}", categoria);

            if (result.Success)
                return RedirectToAction("Index");
            else
                return View("Cadastrar", categoria);
        }

        public JsonResult Upload(IFormFile DocumentPhotos)
        {
            return Json("");
        }
    }
}