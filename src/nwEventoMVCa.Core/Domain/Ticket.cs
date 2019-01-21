using System;
using System.Collections.Generic;
using System.Text;

namespace nwEventoMVCa.Core.Domain
{
    public class Ticket
    {
        public Guid Id { get; protected set; }

        public Guid EventId { get; protected set; }

        public string EventName { get; protected set; }

        public int Seating { get; protected set; }

        public Guid? UserId { get; protected set; }

        public string Username { get; protected set; }

        public DateTime? PurchasedAt { get; protected set; }

        public bool Purchased => PurchasedAt.HasValue;

        public Ticket(Event @event, int seating)
        {
            Id = Guid.NewGuid();
            EventId = @event.Id;
            Seating = seating;
            EventName = @event.Name;
        }

        public void Purchase(User user)
        {
            if (Purchased)
            {
                throw new Exception("Ticket was already purchased");
            }
            UserId = user.Id;
            PurchasedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            if (!Purchased)
            {
                throw new Exception("Ticket was not purchased");
            }
            UserId = null;
            Username = null;
            PurchasedAt = null;
        }
    }
}
