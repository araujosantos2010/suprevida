using BioStore.Shared.Commands;
using FluentValidator;
using System;

namespace BioStore.Domain.StoreContext.Commands.GradeCommands.Inputs
{
    public class CriarVariacaoCommand : Notifiable, ICommand
    {
        public CriarGradeCommand Grade { get; set; }
        public Guid VariacaoId { get; set; }
        public string Nome { get; set; }
        
        bool ICommand.Valid()
        {
            return IsValid;
        }
    }
}
