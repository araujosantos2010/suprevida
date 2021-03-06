using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioStore.Admin.Models
{
    public class GradeViewModel
    {
        public Guid GradeId { get; set; }
        public string Nome { get; set; }
        public List<VariacaoViewModel> Variacao { get; set; }
    }
}
