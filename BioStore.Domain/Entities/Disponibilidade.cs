using BioStore.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BioStore.Domain.StoreContext.Entities
{
    public class Disponibilidade
    {
        public Disponibilidade(Guid disponibilidadeId)
        {
            DisponibilidadeId = disponibilidadeId;
        }

        public Disponibilidade(Guid disponibilidadeId, string dias)
        {
            DisponibilidadeId = disponibilidadeId;
            Dias = dias;
        }

        public Guid DisponibilidadeId { get; private set; }
        public string Dias { get; private set; }

        public override string ToString()
        {
            return $"Disponibilidade para {Dias} dias";
        }
    }
}
