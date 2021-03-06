using System;
using System.Collections.Generic;
using BioStore.Domain.StoreContext.Entities;
using BioStore.Domain.StoreContext.Enums;
using BioStore.Domain.StoreContext.Queries;

namespace BioStore.Domain.StoreContext.Repositories
{
    public interface ICategoriaRepository
    {   
        void Salvar(Categoria marca);
        List<ListaDeCategoriaResult> ObterTodos();
        bool CheckCategoriaPorNome(string nome);
        bool CheckCategoriaPorNomeEId(string nome, Guid marcaId);
        CategoriaResult ObterPorId(Guid marcaId);
        void Atualizar(Categoria marca);
        IList<ListaDeCategoriaResult> ObterPaginacao(int pageNumber, int pageSize);
        void HabilitaOuDesabilitar(ECategoriaStatus status, Guid id);
        IList<ListaDeCategoriaResult> ObterPorNone(string idtermo);
    }
}