
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BioStore.Admin.Models
{
    public class ProdutoViewModel
    {
       
        public Guid ProdutoId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor insira um nome")]        
        public string Nome { get; set; }
        public IFormFile file { get; set; }
        public IFormFile file1 { get; set; }
        public IFormFile file2 { get; set; }
        public IFormFile file3 { get; set; }
        public List<string> Arquivos { get; set; } = new List<string>();
     
      
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor insira o sku")]
        public string Sku { get; set; }
        public decimal? PrecoDeCusto { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor insira o preço")]
        public decimal PrecoDeVenda { get; set; }

        [Remote(action: "PrecoDoProduto", controller: "Produto", AdditionalFields = nameof(PrecoDeVenda))]
        public decimal? PrecoPromocional { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor insira uma marca")]
        public Guid MarcaId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor insira uma categoria")]
        public List<Guid> CategoriaId { get; set; } 
        public string UrlProdutoYouTube { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor insira uma descrição")]
        public string Descricao { get; set; }
        public EProdutoStatusViewModel Status { get; set; }

        //SEO
        public string TagTitle { get; set; }
        public string MetaTagDescription { get; set; }
        public string MetaTagKeywords { get; set; }

        //Peso Dimensoes
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor insira o peso")]
        public float Peso { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor insira a altura")]
        public float Altura { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor insira a largura")]
        public float Largura { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor insira a profundidade")]
        public float Profundidade { get; set; }
        public decimal QuantidadeDisponivel { get; set; } 
        public string diretorio { get; set; }        
    }
}
