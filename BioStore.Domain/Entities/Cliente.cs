using BioStore.Domain.StoreContext.ValueObjects;
using FluentValidator;
using System.Collections.Generic;
using System.Linq;
using BioStore.Shared.Entities;
using BioStore.Domain.StoreContext.Enums;
using System;

namespace BioStore.Domain.StoreContext.Entities
{
    public class Cliente : Entity
    {
        private readonly IList<Endereco> _addresses;

        public Cliente(
            Nome nome,
            Documento documento,
            Email email,
            string telefone)
        {
            Name = nome;
            Documento = documento;
            Email = email;
            Telefone = telefone;
            _addresses = new List<Endereco>();
        }

        public Nome Name { get; private set; }
        public Documento Documento { get; private set; }
        public Email Email { get; private set; }
        public string Telefone { get; private set; }
        public ESexo Sexo { get; private set; }
        public DateTime DataDeNascimento { get; private set; }
        public EClienteStatus Status { get; private set; }

        public IReadOnlyCollection<Endereco> Addresses => _addresses.ToArray();

        public void AddAddress(Endereco address)
        {
            _addresses.Add(address);
        }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}