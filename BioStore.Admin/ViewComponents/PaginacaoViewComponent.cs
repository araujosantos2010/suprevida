using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BioStore.Admin.ViewComponents
{
    public class PaginacaoViewComponent : ViewComponent
    {   
        public async Task<IViewComponentResult> InvokeAsync(dynamic model, string action, string controller, string tag)
        {
            ViewBag.Action = action;
            ViewBag.Controller = controller;
            ViewBag.TagAjax = tag;
            return View(await Task.FromResult(model));
        }
    }
}
