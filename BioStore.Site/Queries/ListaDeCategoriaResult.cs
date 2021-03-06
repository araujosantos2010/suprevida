using BioStore.Site.Models;
using System;

namespace BioStore.Site.Queries
{
    public class ListaDeCategoriaResult
    {
        public Guid CategoriaId { get; set; }
        public string Nome { get; set; }

        private string _destaque;

        public string Destaque
        {
            get { return _destaque == "true" ? "Sim" : "Não"; }
            set { _destaque = value; }
        }

        public ECategoriaStatusViewModel Status { get; set; }
    }
}
