using BioStore.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BioStore.Domain.StoreContext.Entities
{
    public class Grade 
    {
        public Grade(string nome)
        {
            GradeId = Guid.NewGuid();
            Nome = nome;
        }

        public Grade(Guid gradeId)
        {
            GradeId = gradeId;
        }

        public Grade(Guid gradeId, string nome)
        {
            GradeId = gradeId;
            Nome = nome;
        }

        public Guid GradeId { get; private set; }
        public string Nome { get; private set; }
        public List<Variacao> Variacao { get; private set; }
    }
}
