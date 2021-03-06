using BioStore.Domain.StoreContext.Entities;
using BioStore.Domain.StoreContext.Enums;
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
    public class GradeRepository : IGradeRepository
    {
        private readonly BioStoreDataContext _context;

        public GradeRepository(BioStoreDataContext context)
        {
            _context = context;
        }

        public void Atualizar(Grade Grade)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Nome", Grade.Nome);

            var query = $"UPDATE Grade set Nome = @Nome WHERE GradeId = '{Grade.GradeId.ToString()}'";

            _context.Connection.Execute(query, parameters);
        }

        public bool CheckGradePorNome(string nome)
        {
            var query = $"select GradeId, Nome from Grade where Nome = '{nome}'";
            var resultado = _context.Connection.Query<ListaDeGradeResult>(query);
            return resultado.Count() > 0;
        }

        public bool CheckGradePorNomeEId(string nome, Guid GradeId)
        {
            var query = $"select GradeId, Nome from Grade where Nome = '{nome}' and GradeId <> '{GradeId}'";
            var resultado = _context.Connection.Query<ListaDeGradeResult>(query);
            return resultado.Count() > 0;
        }

        public bool CheckVariacaoPorNomeGrade(string nome, Guid gradeId)
        {
            var query = $"select GradeId, Nome from Grade where Nome = '{nome}' and GradeId = '{gradeId.ToString()}'";
            var resultado = _context.Connection.Query<ListaDeGradeResult>(query);
            return resultado.Count() > 0;
        }

        public int ExcluirVariacao(Guid id)
        {
            var total = _context.Connection.Execute("spCriarDeletarVariacao",
            new
            {
                @VariacaoId = id                
            }, commandType: CommandType.StoredProcedure);

            return total;
        }

        public IList<ListaDeGradeResult> ObterPaginacao(int pageNumber, int pageSize)
        {
                var para = new DynamicParameters();
                para.Add("@PageNumber", pageNumber);
                para.Add("@PageSize", pageSize);
                var data = _context.Connection.Query<ListaDeGradeResult>("GradePaginacao", para, commandType: CommandType.StoredProcedure).ToList();
                return data;

        }

        public GradeResult ObterPorId(Guid GradeId)
        {
            var query = $"select GradeId, Nome from Grade where GradeId = '{GradeId}'";
            var resultado = _context.Connection.QueryFirstOrDefault<GradeResult>(query);
            return resultado;
        }

        public List<ListaDeGradeResult> ObterTodos()
        {
            var query = "select GradeId, Nome from Grade";
            var resultado = _context.Connection.Query<ListaDeGradeResult>(query);
            return resultado.ToList();
        }

        public IList<ListaDeVariacaoResult> ObterVariacaoPorGrade(Guid id)
        {
            var query = $@" select a.VariacaoId, a.Nome, COUNT(b.ProdutoId) as QuantidadeDeProdutosVinculados, a.GradeId  from Variacao a 
                        left join produto b on a.VariacaoId = b.VariacaoId 
	                    where a.GradeId = '{id}'
	                    group by  a.VariacaoId, a.Nome, a.GradeId";
            var resultado = _context.Connection.Query<ListaDeVariacaoResult>(query).ToList();
            return resultado;
        }

        public void Salvar(Grade Grade)
        {
            _context.Connection.Execute("spCriarGrade",
            new
            {
                @GradeId = Grade.GradeId,
                @Nome = Grade.Nome              
            }, commandType: CommandType.StoredProcedure);
        }

        public void SalvarVariacao(Variacao variacao)
        {
            _context.Connection.Execute("spCriarVariacao",
            new
            {                @GradeId = variacao.Grade.GradeId,
                @VariacaoId = variacao.VariacaoId,
                @Nome = variacao.Nome
            }, commandType: CommandType.StoredProcedure);
        }
    }
}