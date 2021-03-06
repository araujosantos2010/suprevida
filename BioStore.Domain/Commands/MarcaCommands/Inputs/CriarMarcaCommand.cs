using BioStore.Domain.StoreContext.Enums;
using BioStore.Shared.Commands;
using FluentValidator;
using System;
using System.IO;

namespace BioStore.Domain.StoreContext.Commands.MarcaCommands.Inputs
{
    public class CriarMarcaCommand : Notifiable, ICommand
    {
        public Guid MarcaId { get; set; }
        public string Nome { get; set; }
        public bool Destaque { get; set; }
        public string arquivo { get; set; }
        public string Logo { get; set; }
        public string Descricao { get; set; }
        public EMarcaStatus Status { get; set; }

        bool ICommand.Valid()
        {
            return IsValid;
        }
    }
}
