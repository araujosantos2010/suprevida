using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BioStore.Admin.Models;
using BioStore.Admin.Queries;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using BioStore.Admin.Helper;
using static BioStore.Admin.Helper.RazorHelper;
using System.Buffers;
using Newtonsoft.Json;

namespace BioStore.Admin.Controllers
{
    public class ProdutoController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private List<VariacaoViewModel> _variacoes = new List<VariacaoViewModel>();
        public ProdutoController(IHostingEnvironment hostingEnvironment)
            => (_hostingEnvironment) = (hostingEnvironment);

        private Random Sku = new Random();
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {          
            ViewBag.Marcas = ObterMarcas();             
            ViewBag.TodasCategorias = ObterCategorias();          

            var produto = new ProdutoViewModel();          
            return View(produto);
        }

        private List<SelectListItem> Grades()
        {
            var TodasGrade = GetApiMethod<IList<ListaDeGradeResult>>("v1/Grade", null);

            var grade = new List<SelectListItem>
            {
                new SelectListItem { Text = "Selecione", Value = "" }
            };

            foreach (var item in TodasGrade)
            {
                grade.Add(new SelectListItem { Text = item.Nome, Value = item.GradeId.ToString() });
            }

            return grade;

        }

        [HttpPost]
        public IActionResult Cadastrar(ProdutoViewModel produto)
        {
            var webRoot = _hostingEnvironment.WebRootPath;
            var fullFilePath = $"{webRoot}/produto/imagens/";

            if (produto.file != null)
                if (produto.file != null)
                {                 
                    using (var ms = new MemoryStream())
                    {
                        produto.file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        produto.Arquivos.Add(Convert.ToBase64String(fileBytes));
                    }                  
                }

            if (produto.file1 != null)
                if (produto.file1 != null)
                {                   
                    using (var ms = new MemoryStream())
                    {
                        produto.file1.CopyTo(ms);
                        var fileBytes1 = ms.ToArray();
                        produto.Arquivos.Add(Convert.ToBase64String(fileBytes1));
                    }
                }

            if (produto.file2 != null)
                if (produto.file2 != null)
                {               
                    using (var ms = new MemoryStream())
                    {
                        produto.file2.CopyTo(ms);
                        var fileBytes2 = ms.ToArray();
                        produto.Arquivos.Add(Convert.ToBase64String(fileBytes2));
                    }
                }

            if (produto.file3 != null)
                if (produto.file3 != null)
                {                   
                    using (var ms = new MemoryStream())
                    {
                        produto.file3.CopyTo(ms);
                        var fileBytes3 = ms.ToArray();
                        produto.Arquivos.Add(Convert.ToBase64String(fileBytes3));
                    }
                }
            
            produto.diretorio = fullFilePath;

            var result = PostApiMethodRsult("v1/Produto", produto);
            if (result.Success)
                return RedirectToAction("Index");
            else
                return View(produto);
        }

        private IList<SelectListItem> Disponibilidade()
        {
            var TodasDisponibilidade= GetApiMethod<IList<DisponibilidadeViewModel>>($"v1/Produto/Disponibilidade", null);

            var disponibilidade = new List<SelectListItem>
            {
                new SelectListItem { Text = "Selecione", Value = "" }
            };

            foreach (var item in TodasDisponibilidade)
            {
                disponibilidade.Add(new SelectListItem { Text = item.ToString(), Value = item.DisponibilidadeId.ToString() });
            }

            return disponibilidade;
        }

        private List<SelectListItem> ObterCategorias()
        {
            var TodasCategorias = GetApiMethod<IList<CategoriaViewModel>>($"v1/Categoria", null);

            var categorias = new List<SelectListItem>
            {
                new SelectListItem { Text = "Selecione", Value = "" }
            };

            foreach (var item in TodasCategorias)
            {
                categorias.Add(new SelectListItem { Text = item.Nome, Value = item.CategoriaId.ToString() });
            }

            return categorias;
        }

        private List<SelectListItem> ObterMarcas()
        {
            var TodasMarcas = GetApiMethod<IList<ListaDeMarcaResult>>("v1/Marca", null);

            var marcas = new List<SelectListItem>
            {
                new SelectListItem { Text = "Selecione", Value = "" }
            };

            foreach (var item in TodasMarcas)
            {
                marcas.Add(new SelectListItem { Text = item.Nome, Value = item.MarcaId.ToString() });
            }
            return marcas;
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult PrecoDoProduto(decimal PrecoDeVenda, decimal PrecoPromocional)
        {
            if (PrecoDeVenda < PrecoPromocional)
            {
                return Json($"Valor deve ser menor ou igual a R$ {PrecoDeVenda.ToString("n2")}.");
            }

            return Json(true);
        }

        [AcceptVerbs("GET")]
        public IActionResult GerarCodigo()
        {   
            var codigo = ObterCodigo();
            return Json(codigo);
        }

        // GET: Transaction/AddOrEdit(Insert)
        // GET: Transaction/AddOrEdit/5(Update)
        [NoDirectAccess]
        public async Task<IActionResult> AdicionarOuEditarVariacaoProduto(int id = 0)
        {    
            ViewBag.Grades = Grades();

            if (id == 0)
                return View(new VariacaoViewModel());
            else
            {
                var transactionModel =   View(new VariacaoViewModel()); //await _context.Transactions.FindAsync(id);
                if (transactionModel == null)
                {
                    return NotFound();
                }
                return View(transactionModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarOuEditarVariacaoProduto(Guid VariacaoId, VariacaoViewModel variacaoViewModel)
        {
            
            if (TempData.ContainsKey("Variacao"))
            {
                _variacoes = await Task.FromResult(TempData.Get<List<VariacaoViewModel>>("Variacao"));
            }          

            _variacoes.Add(new VariacaoViewModel { GradeId = variacaoViewModel.GradeId, VariacaoId = variacaoViewModel.VariacaoId, Grade = new GradeViewModel { Nome = $"Grade 1" } });

            TempData.Put("Variacao", _variacoes);
            if (ModelState.IsValid)
            {

                return Json(new { isValid = true, html = RazorHelper.RenderRazorViewToString(this, "_variacaoProduto", _variacoes) });
            }
            

            return Json(new { isValid = false, html = RazorHelper.RenderRazorViewToString(this, "AdicionarOuEditarVariacaoProduto", variacaoViewModel) });

           
        }


        private long ObterCodigo()
        {
            var codigo = Sku.Next();
            var numeroExiste = GetApiMethod<bool>($"v1/produto/codigo/{codigo}", null);
            if (numeroExiste)           
                ObterCodigo();

            return codigo;
        }
    }
}
