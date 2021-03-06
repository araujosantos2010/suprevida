using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioStore.Site.Models
{
    public class DisponibilidadeViewModel
    {
        public Guid DisponibilidadeId { get; set; }
        public int Dias { get; set; }

        public override string ToString()
        {

            return Dias == 0 ? "Disponibilidade Imediata" : $"Disponibilidade para {Dias} dias";
        }
    }
}
