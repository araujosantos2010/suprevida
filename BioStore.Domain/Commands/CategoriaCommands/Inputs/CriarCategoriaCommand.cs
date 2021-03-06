using BioStore.Domain.StoreContext.Enums;
using BioStore.Shared.Commands;
using FluentValidator;
using System;
using System.IO;

namespace BioStore.Domain.StoreContext.Commands.CategoriaCommands.Inputs
{
    public class CriarCategoriaCommand : Notifiable, ICommand
    {
        public Guid CategoriaId { get; set; }
        public CriarCategoriaCommand CategoriaPai { get; set; }
        public string Nome { get; set; }
        public bool Destaque { get; set; }
        public string arquivo { get; set; }
        public string Logo { get; set; }
        public string Descricao { get; set; }
        public ECategoriaStatus Status { get; set; }

        bool ICommand.Valid()
        {
            return IsValid;
        }
    }
}
