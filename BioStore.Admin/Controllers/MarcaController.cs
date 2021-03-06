using BioStore.Admin.Helper;
using BioStore.Admin.Models;
using BioStore.Admin.Queries;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using X.PagedList;

namespace BioStore.Admin.Controllers
{
    public class MarcaController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public MarcaController(IHostingEnvironment hostingEnvironment)
            => (_hostingEnvironment) = (hostingEnvironment);


        [HttpGet]
        public IActionResult Index(int? page = 1)
        {
            //var marcas = GetApiMethod<IList<ListaDeMarcaResult>>("v1/Marca", null);
            //return View(marcas);


            var marcaResult = new MarcaResult();

            if (page < 0)
            {
                page = 1;
            }

            var pageIndex = (page ?? 1) - 1;
            var pageSize = 10;

            int totalProductCount = GetApiMethod<IList<ListaDeMarcaResult>>("v1/Marca", null).Count;
            var marcas = GetApiMethod<IList<ListaDeMarcaResult>>($"v1/Marca/{page}/{pageSize}", null);
            var marcaPagedList = new StaticPagedList<ListaDeMarcaResult>(marcas, pageIndex + 1, pageSize, totalProductCount);
            marcaResult.Marcas = marcaPagedList;
            marcaResult.pageSize = page;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_marcas", marcaResult);
            }

            return View(marcaResult);

        }      

        [HttpGet]
        public IActionResult Cadastrar()
        {
           
            return View();
        }

        [HttpGet("Marca/Editar/{id:guid}")]
        public IActionResult Editar(Guid id)
        {
            var marca = GetApiMethod<MarcaViewModel>($"v1/Marca/{id}", null);            

            return View("Cadastrar", marca);
        }

        [HttpPost]
        public IActionResult Cadastrar(MarcaViewModel marca)
        {
            if (marca.file != null)
                if (marca.file != null)
                {
                    var fileName = marca.file.FileName;
                    var webRoot = _hostingEnvironment.WebRootPath;
                    var fullFilePath = $"{webRoot}/marca/imagens/";
                    var file = Path.Combine(webRoot, fullFilePath);

                    using (var ms = new MemoryStream())
                    {
                        marca.file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        marca.arquivo = Convert.ToBase64String(fileBytes);
                    }
                    marca.Logo = fullFilePath;
                }

            var result = PostApiMethodRsult("v1/Marca", marca);
            if (result.Success)
                return RedirectToAction("Index");
            else
                return View(marca);
           
        }

        [HttpPost]
        public IActionResult Atualizar(MarcaViewModel marca)
        {            

            if (marca.file != null)
            {
                var fileName = marca.file.FileName;
                var webRoot = _hostingEnvironment.WebRootPath;
                var fullFilePath = $"{webRoot}/marca/imagens/";
                var file = Path.Combine(webRoot, fullFilePath);     
                
                using (var ms = new MemoryStream())
                {
                    marca.file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    marca.arquivo  = Convert.ToBase64String(fileBytes);
                }
                marca.Logo = fullFilePath;                
            }

            var result = PutApiMethodResult($"v1/Marca/{marca.MarcaId}", marca);

            if (result.Success)
                return RedirectToAction("Index");
            else
                return View("Cadastrar", marca);
        }

        public JsonResult Upload(IFormFile DocumentPhotos)
        {
            return Json("");
        }
    }
}