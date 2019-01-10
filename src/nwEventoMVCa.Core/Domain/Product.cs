using System;
using System.Collections.Generic;
using System.Text;

namespace nwEventoMVCa.Core.Domain
{
    public class Product
    {
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }


        public Product(string name)
        {
            Id = Guid.NewGuid();
            SetName(name);
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
    }
}
