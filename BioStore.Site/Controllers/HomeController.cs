using BioStore.Site.Helper;
using BioStore.Site.Models;
using BioStore.Site.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using X.PagedList;

namespace BioStore.Site.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            var produtos = GetApiMethod<IList<ListaDeMarcaResult>>($"v1/Produto/ObterProdutos", null);
            return View(produtos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
