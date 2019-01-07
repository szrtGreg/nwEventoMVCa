using System;
using System.Collections.Generic;
using System.Text;

namespace nwEventoMVCa.Core.DTO
{
    public class EventDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        public int TicketsCount { get; set; }

        public int AvailableTicketsCount { get; set; }

        public int PurchasedTicketsCount { get; set; }
    }
}
