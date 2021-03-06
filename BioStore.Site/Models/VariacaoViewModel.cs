using System;

namespace BioStore.Site.Models
{
    public class VariacaoViewModel
    {
        public Guid VariacaoId { get; set; }
        public Guid GradeId { get; set; }
        
        public string Nome { get; set; }
        public GradeViewModel Grade { get; set; }

    }
}
