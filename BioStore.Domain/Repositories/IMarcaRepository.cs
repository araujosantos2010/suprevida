using System;
using System.Collections.Generic;
using BioStore.Domain.StoreContext.Entities;
using BioStore.Domain.StoreContext.Enums;
using BioStore.Domain.StoreContext.Queries;

namespace BioStore.Domain.StoreContext.Repositories
{
    public interface IMarcaRepository
    {   
        void Salvar(Marca marca);
        List<ListaDeMarcaResult> ObterTodos();
        bool CheckMarcaPorNome(string nome);
        bool CheckMarcaPorNomeEId(string nome, Guid marcaId);
        MarcaResult ObterPorId(Guid marcaId);
        void Atualizar(Marca marca);
        IList<ListaDeMarcaResult> ObterPaginacao(int pageNumber, int pageSize);
        void HabilitaOuDesabilitar(EMarcaStatus status, Guid id);
    }
}