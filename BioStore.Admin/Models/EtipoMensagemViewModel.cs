using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BioStore.Admin.Models
{
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
