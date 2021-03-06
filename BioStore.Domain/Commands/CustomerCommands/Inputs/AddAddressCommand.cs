using System;
using BioStore.Domain.StoreContext.Enums;
using BioStore.Shared.Commands;
using FluentValidator;

namespace BioStore.Domain.StoreContext.CustomerCommands.Inputs
{
    public class AddAddressCommand : Notifiable, ICommand
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string Number { get;  set; }
        public string Complement { get; set; }
        public string District { get;  set; }
        public string City { get;  set; }
        public string State { get;  set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public ETipoEndereco Type { get; set; }

        bool ICommand.Valid()
        {
            return IsValid;
        }
    }
}