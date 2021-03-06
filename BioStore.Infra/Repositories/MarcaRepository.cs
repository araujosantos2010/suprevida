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
    public class MarcaRepository : IMarcaRepository
    {
        private readonly BioStoreDataContext _context;

        public MarcaRepository(BioStoreDataContext context)
        {
            _context = context;
        }

        public void Atualizar(Marca marca)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Nome", marca.Nome);
            parameters.Add("@Destaque", marca.Destaque);
            if (!string.IsNullOrEmpty(marca.Logo))
            {
                parameters.Add("@Logo", $"/marca/imagens/{marca.MarcaId}.png");
            }   
            parameters.Add("@Descricao", marca.Descricao);
            parameters.Add("@Status", marca.Status.GetHashCode());            


            var query = @"UPDATE Marca set Nome = @Nome
                              ,Destaque = @Destaque"; 
                              if(!string.IsNullOrEmpty(marca.Logo)) { query += ",Logo = @Logo"; }
                query += $@",Descricao = @Descricao
                              ,Status = @Status
                         WHERE MarcaId = '{marca.MarcaId.ToString()}'";

            _context.Connection.Execute(query, parameters);
        }

        public bool CheckMarcaPorNome(string nome)
        {
            var query = $"select MarcaId, Nome, Destaque, Status from Marca where Nome = '{nome}'";
            var resultado = _context.Connection.Query<ListaDeMarcaResult>(query);
            return resultado.Count() > 0;
        }

        public bool CheckMarcaPorNomeEId(string nome, Guid MarcaId)
        {
            var query = $"select MarcaId, Nome, Destaque, Status from Marca where Nome = '{nome}' and MarcaId <> '{MarcaId}'";
            var resultado = _context.Connection.Query<ListaDeMarcaResult>(query);
            return resultado.Count() > 0;
        }

        public void HabilitaOuDesabilitar(EMarcaStatus status, Guid id)
        {
            var parameters = new DynamicParameters();
          
            parameters.Add("@Status", status.GetHashCode());

            var query = $"UPDATE Marca set  ,Status = @Status  WHERE MarcaId = '{id.ToString()}'";

            _context.Connection.Execute(query, parameters);
        }

        public IList<ListaDeMarcaResult> ObterPaginacao(int pageNumber, int pageSize)
        {
                var para = new DynamicParameters();
                para.Add("@PageNumber", pageNumber);
                para.Add("@PageSize", pageSize);
                var data = _context.Connection.Query<ListaDeMarcaResult>("MarcaPaginacao", para, commandType: CommandType.StoredProcedure).ToList();
                return data;

        }

        public MarcaResult ObterPorId(Guid marcaId)
        {
            var query = $"select MarcaId, Nome, Destaque, Logo, Status, descricao from Marca where MarcaId = '{marcaId}'";
            var resultado = _context.Connection.QueryFirstOrDefault<MarcaResult>(query);
            return resultado;
        }

        public List<ListaDeMarcaResult> ObterTodos()
        {
            var query = "select MarcaId, Nome, Destaque, Status from Marca";
            var resultado = _context.Connection.Query<ListaDeMarcaResult>(query);
            return resultado.ToList();
        }

        public void Salvar(Marca marca)
        {
            _context.Connection.Execute("spCriarMarca",
            new
            {
                @MarcaId = marca.MarcaId,
                @Nome = marca.Nome,
                @Status = marca.Status.GetHashCode(),
                @Destaque = marca.Destaque,
                @Descricao = marca.Descricao,
                @Logo = marca.Logo
            }, commandType: CommandType.StoredProcedure);
        }
    }
}