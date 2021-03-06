using BioStore.Domain.StoreContext.Enums;
using System;
using System.Collections.Generic;

namespace BioStore.Domain.StoreContext.Queries
{
    public class ProdutoResult
    {
        public Guid ProdutoId { get; private set; }
        public string Nome { get; private set; }
        public List<string> Imagens { get; private set; } 
        public bool ProdutoPossuiVariacao { get; private set; }
        public bool ProdutoEmDestaque { get; private set; }
        public bool ProdutoNovo { get; private set; }
        public string Sku { get; private set; }
        public decimal PrecoDeCusto { get; private set; }
        public decimal PrecoDeVenda { get; private set; }
        public decimal PrecoPromocional { get; private set; }
        public MarcaResult Marca { get; set; }
        public List<CategoriaResult> Categoria { get; set; }
        public string UrlProdutoYouTube { get; set; }
        public string Descricao { get; private set; }

        //SEO
        public string TagTitle { get; private set; }
        public string MetaTagDescription { get; private set; }
        public string MetaTagKeywords { get; private set; }

        //Peso Dimensoes
        public float Peso { get; private set; }
        public float Altura { get; private set; }
        public float Largura { get; private set; }
        public float Profundidade { get; private set; }

    }
}
