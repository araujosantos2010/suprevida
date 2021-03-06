using BioStore.Domain.StoreContext.Enums;
using System;

namespace BioStore.Domain.StoreContext.Queries
{
    public class CategoriaResult
    {
        public Guid CategoriaId { get; private set; }
        public Guid CategoriaPaiId { get; private set; }
        public string Nome { get; private set; }
        public bool Destaque { get; private set; }
        public string Logo { get; private set; }
        public string Descricao { get; private set; }
        public ECategoriaStatus Status { get; private set; }
    }
}
