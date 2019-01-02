using System;
using System.Collections.Generic;
using System.Text;

namespace nwEventoMVCa.Core.Domain
{
    public class Ticket
    {
        public Guid Id { get; protected set; }

        public Guid EventId { get; protected set; }

        public int Seating { get; protected set; }

        public Ticket(Event @event, int seating)
        {
            Id = Guid.NewGuid();
            EventId = @event.Id;
            Seating = seating;
        }
    }
}
