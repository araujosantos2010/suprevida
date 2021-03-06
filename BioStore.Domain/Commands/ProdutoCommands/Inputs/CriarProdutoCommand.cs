using BioStore.Domain.StoreContext.Enums;
using BioStore.Shared.Commands;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.IO;

namespace BioStore.Domain.StoreContext.Commands.MarcaCommands.Inputs
{
    public class CriarProdutoCommand : Notifiable, ICommand
    {
        public Guid ProdutoId { get; set; }        
        public string Nome { get; set; }        
        public List<string> Arquivos { get; set; } 
        public bool ProdutoPossuiVariacao { get; set; }
        public bool ProdutoEmDestaque { get; set; }
        public bool ProdutoNovo { get; set; }        
        public string Sku { get; set; }
        public decimal? PrecoDeCusto { get; set; }
        public decimal PrecoDeVenda { get; set; }
        public decimal? PrecoPromocional { get; set; }
        public Guid MarcaId { get; set; }
        public List<Guid> CategoriaId { get; set; }
        public string UrlProdutoYouTube { get; set; }        
        public string Descricao { get; set; }
        //public EProdutoStatus Status { get; set; }

        //SEO
        public string TagTitle { get; set; }
        public string MetaTagDescription { get; set; }
        public string MetaTagKeywords { get; set; }

        //Peso Dimensoes
        public float Peso { get; set; }        
        public float Altura { get; set; }        
        public float Largura { get; set; }        
        public float Profundidade { get; set; }

        //Esqoque
        public bool GerenciarEstoqueDesseProduto { get; set; }
        public decimal QuantidadeDisponivel { get; set; }
        public Guid DisponibilideId { get; set; }
        public Guid FimDisponibilideId { get; set; }

        public string diretorio { get; set; }

        public Guid GradeId { get; set; }

        public List<Guid> VariacaoId { get; set; }

        bool ICommand.Valid()
        {
            return IsValid;
        }
    }
}
