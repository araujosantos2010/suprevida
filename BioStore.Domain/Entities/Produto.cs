using BioStore.Shared.Entities;
using FluentValidator;
using System;
using System.Collections.Generic;

namespace BioStore.Domain.StoreContext.Entities
{
    public class Produto 
    {
        public Produto()
        {
            ProdutoId = Guid.NewGuid();
        }

        public Produto(string nome, List<string> imagens, bool produtoPossuiVariacao, bool produtoEmDestaque, bool produtoNovo, string sku, decimal? precoDeCusto, decimal precoDeVenda, decimal? precoPromocional, Marca marca, List<Categoria> categoria, string urlProdutoYouTube, string descricao, float peso, float altura, float largura, float profundidade, bool gerenciarEstoqueDesseProduto, decimal quantidadeDisponivel, string tagTitle, string metaTagDescription, string metaTagKeywords)
        {
            ProdutoId = Guid.NewGuid();
            Nome = nome;
            Imagens = imagens;
            ProdutoPossuiVariacao = produtoPossuiVariacao;
            ProdutoEmDestaque = produtoEmDestaque;
            ProdutoNovo = produtoNovo;
            Sku = sku;
            PrecoDeCusto = precoDeCusto;
            PrecoDeVenda = precoDeVenda;
            PrecoPromocional = precoPromocional;
            Marca = marca;
            Categoria = categoria;
            UrlProdutoYouTube = urlProdutoYouTube;
            Descricao = descricao;
            Peso = peso;
            Altura = altura;
            Largura = largura;
            Profundidade = profundidade;
            GerenciarEstoqueDesseProduto = gerenciarEstoqueDesseProduto;
            QuantidadeDisponivel = quantidadeDisponivel;
            TagTitle = tagTitle;
            MetaTagDescription = metaTagDescription;
            MetaTagKeywords = metaTagKeywords;
        }

        public Produto(Guid produtoId, string nome, List<string> imagens, bool produtoPossuiVariacao, bool produtoEmDestaque, bool produtoNovo, string sku, decimal? precoDeCusto, decimal precoDeVenda, decimal? precoPromocional, Marca marca, List<Categoria> categoria, string urlProdutoYouTube, string descricao, float peso, float altura, float largura, float profundidade, bool gerenciarEstoqueDesseProduto, decimal quantidadeDisponivel, string tagTitle, string metaTagDescription, string metaTagKeywords)
        {
            ProdutoId = produtoId;
            Nome = nome;
            Imagens = imagens;
            ProdutoPossuiVariacao = produtoPossuiVariacao;
            ProdutoEmDestaque = produtoEmDestaque;
            ProdutoNovo = produtoNovo;
            Sku = sku;
            PrecoDeCusto = precoDeCusto;
            PrecoDeVenda = precoDeVenda;
            PrecoPromocional = precoPromocional;
            Marca = marca;
            Categoria = categoria;
            UrlProdutoYouTube = urlProdutoYouTube;
            Descricao = descricao;
            Peso = peso;
            Altura = altura;
            Largura = largura;
            Profundidade = profundidade;
            GerenciarEstoqueDesseProduto = gerenciarEstoqueDesseProduto;
            QuantidadeDisponivel = quantidadeDisponivel;           
            TagTitle = tagTitle;
            MetaTagDescription = metaTagDescription;
            MetaTagKeywords = metaTagKeywords;           
        }

        public Guid ProdutoId { get; private set; }
        public string Nome { get; private set; }
        public List<string> Imagens { get; private set; } 
        public bool ProdutoPossuiVariacao { get; private set; }
        public bool ProdutoEmDestaque { get; private set; }
        public bool ProdutoNovo { get; private set; }
        public string Sku { get; private set; }
        public decimal? PrecoDeCusto { get; private set; }
        public decimal PrecoDeVenda { get; private set; }
        public decimal? PrecoPromocional { get; private set; }
        public Marca Marca { get; set; }
        public List<Categoria> Categoria { get; set; } 
        public string UrlProdutoYouTube { get; set; }
        public string Descricao { get; private set; }       

        //Peso Dimensoes
        public float Peso { get; private set; }
        public float Altura { get; private set; }
        public float Largura { get; private set; }
        public float Profundidade { get; private set; }

        //Esqoque
        public bool GerenciarEstoqueDesseProduto { get; private set; }
        public decimal QuantidadeDisponivel { get; private set; }     


        //SEO
        public string TagTitle { get; private set; } = null;
        public string MetaTagDescription { get; private set; } = null;
        public string MetaTagKeywords { get; private set; } = null;

        public override string ToString()
        {
            return Nome;
        }

        public void DiminuirQuantidade(decimal quantidade)
        {
            QuantidadeDisponivel -= quantidade;
        }
    }
}