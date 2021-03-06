using System;

namespace BioStore.Domain.StoreContext.Queries
{
    public class ListaDeVariacaoResult
    {
        public int QuantidadeDeProdutosVinculados { get; set; }
        public Guid VariacaoId { get; set; }
        public Guid GradeId { get; set; }
        public string Nome { get; set; }
    }
}
