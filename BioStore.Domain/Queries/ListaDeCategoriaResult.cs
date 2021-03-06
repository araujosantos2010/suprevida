using BioStore.Domain.StoreContext.Enums;
using System;

namespace BioStore.Domain.StoreContext.Queries
{
    public class ListaDeCategoriaResult
    {
        public Guid CategoriaId { get; private set; }
        public Guid CategoriaPaiId { get; private set; }
        public string Nome { get; private set; }
        public bool Destaque { get; private set; }                
        public ECategoriaStatus Status { get; private set; }
    }
}
