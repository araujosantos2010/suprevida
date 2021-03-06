using BioStore.Domain.StoreContext.Enums;
using System;

namespace BioStore.Domain.StoreContext.Entities
{
    public class Marca
    {
        public Marca(Guid marcaId)
        {
            MarcaId = marcaId;
        }

        public Marca(string nome, bool destaque, string logo, string descricao, EMarcaStatus status)
        {
            MarcaId = Guid.NewGuid();
            Nome = nome;
            Destaque = destaque;
            Logo = logo;
            Descricao = descricao;
            Status = status;
        }

        public Marca(Guid marcaId, string nome, bool destaque, string logo, string descricao, EMarcaStatus status)
        {
            MarcaId = marcaId;
            Nome = nome;
            Destaque = destaque;
            Logo = logo;
            Descricao = descricao;
            Status = status;
        }

        public Guid MarcaId { get; private set; }
        public string Nome { get; private set; }
        public bool Destaque { get; private set; }
        public string Logo { get; private set; }
        public string Descricao { get; private set; }
        public EMarcaStatus Status { get; private set; }
    }
}
