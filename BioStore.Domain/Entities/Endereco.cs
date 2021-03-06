using BioStore.Domain.StoreContext.Enums;
using BioStore.Shared.Entities;
using FluentValidator;
using System;

namespace BioStore.Domain.StoreContext.Entities
{
    public class Endereco : Entity
    {
        public Endereco(string logradrouro, string numero, string complemento, string bairro, string cidade, string estado, string cep, ETipoEndereco tipo, DateTime dataDenascimento, EClienteStatus status)
        {
            Logradrouro = logradrouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Cep = cep;
            Tipo = tipo;          
           
        }

        public string Logradrouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }        
        public string Cep { get; private set; }
        public ETipoEndereco Tipo { get; private set; }       
        
        public override string ToString()
        {
            return $"{Logradrouro}, {Numero} - {Cidade}/{Estado}";
        }
    }
}
