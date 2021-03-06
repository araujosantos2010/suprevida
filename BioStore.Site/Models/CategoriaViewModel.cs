using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace BioStore.Site.Models
{
    public class CategoriaViewModel
    {
        public Guid CategoriaId { get; set; }
        public Guid CategoriaPaiId { get; set; }
        public CategoriaViewModel CategoriaPai { get; set; }
        public string Nome { get; set; }
        public bool Destaque { get; set; }
        public string Descricao { get; set; }        
        public ECategoriaStatusViewModel Status { get; set; }
    }
}
