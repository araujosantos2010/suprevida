using BioStore.Domain.StoreContext.Enums;
using BioStore.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BioStore.Domain.StoreContext.Entities
{
    public class Categoria 
    {
        public Categoria(Guid categoriaId)
        {
            CategoriaId = categoriaId;
        }

        public Categoria(ECategoriaStatus status, bool destaque, string nome, string descricao)
        {
            CategoriaId = Guid.NewGuid();
            Status = status;
            Destaque = destaque;
            Nome = nome;
            Descricao = descricao;
        }

        public Categoria(ECategoriaStatus status, bool destaque, string nome, string descricao, Categoria categoriaPai) : this(status, destaque, nome, descricao)
        {
            CategoriaPai = categoriaPai;
        }

        public Categoria(Guid categoriaId, ECategoriaStatus status, bool destaque, string nome, string descricao)
        {
            CategoriaId = categoriaId;
            Status = status;
            Destaque = destaque;
            Nome = nome;
            Descricao = descricao;
        }

        public Categoria(Guid categoriaId, ECategoriaStatus status, bool destaque, string nome, string descricao, Categoria categoriaPai) : this(categoriaId, status, destaque, nome, descricao)
        {
            CategoriaPai = categoriaPai;
        }

        public Guid CategoriaId { get; set; }
        public ECategoriaStatus Status { get; private set; }
        public bool Destaque { get; private set; }        
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public Categoria CategoriaPai { get; private set; }
    }
}
