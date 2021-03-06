using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BioStore.Site.Helper
{
    public class BioResult
    {
        public EtipoMensagemViewModel TipoMensagem { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public bool ExibirMensagem { get; set; } = true;
    }
    public static class HttpExtension
    {
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Headers != null)
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";
            return false;
        }
    }

    public enum EtipoMensagemViewModel
    {
        [Description("success")]
        Success = 1,
        [Description("info")]
        Info = 2,
        [Description("warning")]
        Warning = 3,
        [Description("error")]
        Error = 4
    }
}
