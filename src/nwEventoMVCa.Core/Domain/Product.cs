using System;
using System.Collections.Generic;
using System.Text;

namespace nwEventoMVCa.Core.Domain
{
    public class Product
    {
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }

        public decimal Price { get; protected set; }


        public Product(string name, decimal price)
        {
            Id = Guid.NewGuid();
            SetName(name);
            SetPrice(price);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Product name is empty.", nameof(name));
            }
            if (name.Length > 100)
            {
                throw new ArgumentException($"Product name is too long: '{name.Length}' chars.", nameof(name));
            }
            Name = name;
        }

        public void SetPrice(decimal price)
        {
            if (price < 1 || price > 100000)
            {
                throw new ArgumentException($"Product price is invalid: {price}.", nameof(price));
            }
            Price = price;
        }
    }
}
