using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BioStore.Admin.ViewComponents
{
    public class AlertaViewComponent : ViewComponent
    {   
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var resultado = TempData["Alerta"];
           
            var mensagem = await Task.FromResult(resultado);
            return View(mensagem);
        }
    }
}
