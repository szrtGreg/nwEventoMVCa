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
            Name = name;
            Category = category;
            Price = price;
        }
    }
}
