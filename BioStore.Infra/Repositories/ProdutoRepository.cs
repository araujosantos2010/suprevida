using BioStore.Domain.StoreContext.Entities;
using BioStore.Domain.StoreContext.Queries;
using BioStore.Domain.StoreContext.Repositories;
using BioStore.Infra.DataContexts;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BioStore.Infra.StoreContext.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly BioStoreDataContext _context;

        public ProdutoRepository(BioStoreDataContext context)
        {
            _context = context;
        }

        public void Atualizar(Produto produto)
        {
            throw new NotImplementedException();
        }

        public bool CheckProdutoPorNome(string nome)
        {
            return false;
        }

        public bool CheckProdutoPorNomeEId(string nome, Guid produtoId)
        {
            return false;
        }

        public bool CodigoExiste(long codigo)
        {
            var query = $"select * from Produto where Sku = {codigo}";
            var resultado = _context.Connection.QuerySingleOrDefault<ProdutoResult>(query);
            return resultado != null;
        }

        public List<ListaDeDisponibilidadeResult> Disponibilidade()
        {
            var query = "select DisponibilidadeId, Dias from Disponibilidade order by Dias asc";
            var resultado = _context.Connection.Query<ListaDeDisponibilidadeResult>(query);
            return resultado.ToList();
        }

        public IList<ListaDeProdutoResult> ObterTodos()
        {
            var query = @"SELECT [ProdutoId],[Nome],[PrecoDeVenda]  FROM [dbTeste].[dbo].[Produto]";
            var resultado = _context.Connection.Query<ListaDeProdutoResult>(query);
            return resultado.ToList();
        }

        public void Salvar(Produto produtoSalvar)
        {
            _context.Connection.Execute("spCriarProduto",
            new
            {
                produtoSalvar.ProdutoId,
                produtoSalvar.Nome,  
                produtoSalvar.Sku,
                produtoSalvar.PrecoDeCusto,
                produtoSalvar.PrecoDeVenda,
                produtoSalvar.PrecoPromocional,
                produtoSalvar.Marca.MarcaId,                
                produtoSalvar.UrlProdutoYouTube,
                produtoSalvar.Descricao,
                produtoSalvar.Peso,
                produtoSalvar.Altura,
                produtoSalvar.Largura,
                produtoSalvar.Profundidade,
                produtoSalvar.GerenciarEstoqueDesseProduto,
                produtoSalvar.QuantidadeDisponivel,
                produtoSalvar.TagTitle,
                produtoSalvar.MetaTagDescription,
                produtoSalvar.MetaTagKeywords
               
            }, commandType: CommandType.StoredProcedure);

            //Salvar imagens
            foreach (var caminho in produtoSalvar.Imagens)
            {
                _context.Connection.Execute("spCriarImagensDoProduto",
                new
                {   @ImagemId = Guid.NewGuid(),
                    @Caminho = caminho,
                    produtoSalvar.ProdutoId
                }, commandType: CommandType.StoredProcedure);
            }

            //Salvar Categorias
            foreach (var categoria in produtoSalvar.Categoria)
            {
                _context.Connection.Execute(@"INSERT INTO [dbo].[ProdutoCategoria]
                                           ([ProdudoId]
                                           ,[CategoriaId])
                                     VALUES
                                           (@ProdutoId
                                           ,@CategoriaId)",
               new
               {
                   ProdutoId =  produtoSalvar.ProdutoId,
                   CategoriaId =  categoria.CategoriaId,
                  
               }, commandType: CommandType.Text);

            }
        }
    }
}