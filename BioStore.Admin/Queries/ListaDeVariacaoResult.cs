using System;

namespace BioStore.Admin.Queries
{
    public class ListaDeVariacaoResult
    {
        public Guid GradeId { get; set; }
        public Guid VariacaoId { get; set; }
        public string Nome { get; set; }
        public int QuantidadeDeProdutosVinculados { get; set; }

        public override string ToString()
        {
            return $"{QuantidadeDeProdutosVinculados} produtos vinculados";
        }
    }
}
