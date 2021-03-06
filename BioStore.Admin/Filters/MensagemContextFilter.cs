using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioStore.Admin.Filters
{
    public class MensagemContextFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var cont = context;
            context.HttpContext.Items.Add("Alertar", context.HttpContext.Items["Alerta"]);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var cont = context;
        }
    }
}
