using System;
using System.Collections.Generic;
using BioStore.Domain.StoreContext.Commands.GradeCommands.Inputs;
using BioStore.Domain.StoreContext.Entities;
using BioStore.Domain.StoreContext.Enums;
using BioStore.Domain.StoreContext.Queries;

namespace BioStore.Domain.StoreContext.Repositories
{
    public interface IGradeRepository
    {   
        void Salvar(Grade Grade);
        List<ListaDeGradeResult> ObterTodos();
        bool CheckGradePorNome(string nome);
        bool CheckGradePorNomeEId(string nome, Guid GradeId);
        GradeResult ObterPorId(Guid GradeId);
        void Atualizar(Grade Grade);
        IList<ListaDeGradeResult> ObterPaginacao(int pageNumber, int pageSize);
        IList<ListaDeVariacaoResult> ObterVariacaoPorGrade(Guid id);
        bool CheckVariacaoPorNomeGrade(string nome, Guid gradeId);
        void SalvarVariacao(Variacao variacao);
        int ExcluirVariacao(Guid id);
    }
}