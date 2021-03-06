using FluentValidator;
using System.Collections;
using System.Collections.Generic;
using BioStore.Shared.Entities;

namespace BioStore.Domain.StoreContext.Entities
{
    public class OrderItem : Entity
    {
        public OrderItem(Produto product, decimal quantity)
        {
            Product = product;
            Quantity = quantity;
            Price = product.PrecoDeVenda;

            if (product.QuantidadeDisponivel < quantity)
                AddNotification("Quantity", "Produto fora de estoque");

            product.DiminuirQuantidade(quantity);
        }

        public Produto Product { get; private set; }
        public decimal Quantity { get; private set; }
        public decimal Price { get; private set; }
    }
}