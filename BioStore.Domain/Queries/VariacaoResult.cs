using System;
using System.Collections.Generic;
using System.Text;

namespace BioStore.Domain.StoreContext.Queries
{
    public class VariacaoResult
    {
        public Guid VariacaoId { get; set; }
        public string Nome { get; set; }
        public int QuantidadeDeProdutosVinculados { get; set; }
    }
}
