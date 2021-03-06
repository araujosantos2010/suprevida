using BioStore.Shared.Commands;
using FluentValidator;
using System;

namespace BioStore.Domain.StoreContext.Commands.GradeCommands.Inputs
{
    public class CriarGradeCommand : Notifiable, ICommand
    {
        public Guid GradeId { get; set; }
        public string Nome { get; set; }
        
        bool ICommand.Valid()
        {
            return IsValid;
        }
    }
}
