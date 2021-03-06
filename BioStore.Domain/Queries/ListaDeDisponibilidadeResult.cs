using System;

namespace BioStore.Domain.StoreContext.Queries
{
    public class ListaDeDisponibilidadeResult
    {
        public Guid DisponibilidadeId { get; set; }
        public int Dias { get; set; }
    }
}
