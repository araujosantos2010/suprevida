using BioStore.Domain.StoreContext.Enums;
using System;

namespace BioStore.Domain.StoreContext.Queries
{
    public class ListaDeProdutoResult
    {
        public Guid ProdutoId { get; private set; }
        public string Nome { get; private set; }
        public decimal PrecoDeVenda { get; private set; }                        
    }
}
