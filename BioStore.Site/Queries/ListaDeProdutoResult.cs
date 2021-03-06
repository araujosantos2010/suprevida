using BioStore.Site.Models;
using System;

namespace BioStore.Site.Queries
{
    public class ListaDeProdutoResult
    {
        public Guid ProdutoId { get; set; }
        public string Nome { get; set; }
        public string imagem { get; set; }
    }
}
