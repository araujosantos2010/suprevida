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
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly BioStoreDataContext _context;

        public CategoriaRepository(BioStoreDataContext context)
        {
            _context = context;
        }

        public void Atualizar(Categoria categoria)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Nome", categoria.Nome);
            parameters.Add("@Destaque", categoria.Destaque);           
            parameters.Add("@Descricao", categoria.Descricao);
            parameters.Add("@Status", categoria.Status.GetHashCode());
            parameters.Add("@CategoriaPaiId", categoria.CategoriaPai?.CategoriaId);
            


            var query = $@"UPDATE Categoria set Nome = @Nome
                              ,Destaque = @Destaque
                              ,Descricao = @Descricao
                              ,CategoriaPaiId = @CategoriaPaiId
                              ,Status = @Status
                         WHERE CategoriaId = '{categoria.CategoriaId.ToString()}'";

            _context.Connection.Execute(query, parameters);
        }

        public bool CheckCategoriaPorNome(string nome)
        {
            var query = $"select CategoriaId, CategoriaPaiId, Nome, Destaque, Status from Categoria where Nome = '{nome}'";
            var resultado = _context.Connection.Query<ListaDeCategoriaResult>(query);
            return resultado.Count() > 0;
        }

        public bool CheckCategoriaPorNomeEId(string nome, Guid CategoriaId)
        {
            var query = $"select CategoriaId, CategoriaPaiId, Nome, Destaque, Status from Categoria where Nome = '{nome}' and CategoriaId <> '{CategoriaId}'";
            var resultado = _context.Connection.Query<ListaDeCategoriaResult>(query);
            return resultado.Count() > 0;
        }

        public void HabilitaOuDesabilitar(ECategoriaStatus status, Guid CategoriaId)
        {
            var parameters = new DynamicParameters();
          
            parameters.Add("@Status", status.GetHashCode());

            var query = $"UPDATE Categoria set Status = @Status  WHERE CategoriaId = '{CategoriaId.ToString()}'";

            _context.Connection.Execute(query, parameters);
        }

        public IList<ListaDeCategoriaResult> ObterPaginacao(int pageNumber, int pageSize)
        {
                var para = new DynamicParameters();
                para.Add("@PageNumber", pageNumber);
                para.Add("@PageSize", pageSize);
                var data = _context.Connection.Query<ListaDeCategoriaResult>("CategoriaPaginacao", para, commandType: CommandType.StoredProcedure).ToList();
                return data;

        }

        public CategoriaResult ObterPorId(Guid CategoriaId)
        {
            var query = $"select CategoriaId, CategoriaPaiId, Nome, Destaque, Status, descricao from Categoria where CategoriaId = '{CategoriaId}'";
            var resultado = _context.Connection.QueryFirstOrDefault<CategoriaResult>(query);
            return resultado;
        }

        public IList<ListaDeCategoriaResult> ObterPorNone(string termo)
        {
            var query = $"select CategoriaId, CategoriaPaiId, Nome, Destaque, Status from Categoria where Nome like '%{termo}%'";
            var resultado = _context.Connection.Query<ListaDeCategoriaResult>(query);
            return resultado.ToList();
        }

        public List<ListaDeCategoriaResult> ObterTodos()
        {
            var query = "select CategoriaId, CategoriaPaiId, Nome, Destaque, Status from Categoria";
            var resultado = _context.Connection.Query<ListaDeCategoriaResult>(query);
            return resultado.ToList();
        }

        public void Salvar(Categoria categoria)
        {
            _context.Connection.Execute("spCriarcategoria",
            new
            {
                @CategoriaId = categoria.CategoriaId,
                @CategoriaPaiId = categoria.CategoriaPai.CategoriaId,
                @Nome = categoria.Nome,
                @Status = categoria.Status.GetHashCode(),
                @Destaque = categoria.Destaque,
                @Descricao = categoria.Descricao               
            }, commandType: CommandType.StoredProcedure);
        }
    }
}