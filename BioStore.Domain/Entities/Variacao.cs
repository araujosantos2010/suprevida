using BioStore.Shared.Entities;
using System;
using System.Collections.Generic;

namespace BioStore.Domain.StoreContext.Entities
{
    public class Variacao 
    {
        public Variacao(Guid variacaoId)
        {
            VariacaoId = variacaoId;
        }

        public Variacao(string nome, Grade grade)
        {
            VariacaoId = Guid.NewGuid();
            Nome = nome;
            Grade = grade;
        }

        public Guid VariacaoId { get; private set; }
        public string Nome { get; private set; }
        public Grade Grade { get; private set; }
    }
}
