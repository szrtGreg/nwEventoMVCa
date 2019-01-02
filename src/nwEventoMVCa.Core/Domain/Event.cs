using System;
using System.Collections.Generic;
using System.Text;

namespace nwEventoMVCa.Core.Domain
{
    public class Event
    {
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }

        public string Category { get; protected set; }

        public decimal Price { get; protected set; }

        public Event(string name, string category, decimal price)
        {
            Id = Guid.NewGuid();
            SetName(name);
            SetCategory(category);
            SetPrice(price);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Event name is empty.", nameof(name));
            }
            if (name.Length > 100)
            {
                throw new ArgumentException($"Event name is too long: '{name.Length}' chars.", nameof(name));
            }
            Name = name;
        }

        public void SetCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                throw new ArgumentException("Event category is empty.", nameof(category));
            }
            Category = category;
        }

        public void SetPrice(decimal price)
        {
            if (price < 1 || price > 100000)
            {
                throw new ArgumentException($"Event price is invalid: {price}.", nameof(price));
            }
            Price = price;
        }
    }
}
