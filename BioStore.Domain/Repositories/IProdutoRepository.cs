using BioStore.Domain.StoreContext.Entities;
using BioStore.Domain.StoreContext.Queries;
using System;
using System.Collections.Generic;

namespace BioStore.Domain.StoreContext.Repositories
{
    public interface IProdutoRepository
    {
        List<ListaDeDisponibilidadeResult> Disponibilidade();
        bool CodigoExiste(long codigo);
        bool CheckProdutoPorNome(string nome);
        void Salvar(Produto produtoSalvar);
        bool CheckProdutoPorNomeEId(string nome, Guid produtoId);
        void Atualizar(Produto produto);
        IList<ListaDeProdutoResult> ObterTodos();
    }
}