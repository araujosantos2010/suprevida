using BioStore.Admin.Models;
using System;

namespace BioStore.Admin.Queries
{
    public class ListaDeMarcaResult
    {
        public Guid MarcaId { get; set; }
        public string Nome { get; set; }

        private string _destaque;

        public string Destaque
        {
            get { return _destaque == "true" ? "Sim" : "Não"; }
            set { _destaque = value; }
        }

        public EMarcaStatusViewModel Status { get; set; }
    }
}
