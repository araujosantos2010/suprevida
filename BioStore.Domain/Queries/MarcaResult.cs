using BioStore.Domain.StoreContext.Enums;
using System;

namespace BioStore.Domain.StoreContext.Queries
{
    public class MarcaResult
    {
        public Guid MarcaId { get; private set; }
        public string Nome { get; private set; }
        public bool Destaque { get; private set; }
        public string Logo { get; private set; }
        public string Descricao { get; private set; }
        public EMarcaStatus Status { get; private set; }
    }
}
