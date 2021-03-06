using BioStore.Domain.StoreContext.Enums;
using System;
using System.Collections.Generic;

namespace BioStore.Domain.StoreContext.Queries
{
    public class GradeResult
    {
        public Guid GradeId { get; private set; }
        public string Nome { get; private set; }
        public List<VariacaoResult> Variacao { get; set; }
    }
}
