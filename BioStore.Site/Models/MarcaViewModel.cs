using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace BioStore.Site.Models
{
    public class MarcaViewModel
    {
        public Guid MarcaId { get; set; }
        public string Nome { get; set; }
        public bool Destaque { get; set; }
        public string Logo { get; set; }
        public IFormFile file { get; set; }
        public string arquivo { get; set; }
        public string Descricao { get; set; }        
        public EMarcaStatusViewModel Status { get; set; }
    }
}
