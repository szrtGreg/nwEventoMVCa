using nwEventoMVCa.Core.Domain;
using nwEventoMVCa.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nwEventoMVCa.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private static readonly ISet<Event> _events = new HashSet<Event>()
        {
            new Event("Laptop", "Platne", 3000),
            new Event("Jeans", "darmowe", 150),
            new Event("Hammer", "Platne", 47)
        };


        public Event Get(Guid id)
            => _events.SingleOrDefault(x => x.Id == id);

        public IEnumerable<Event> GetAll()
            => _events;

        public void Add(Event @event)
            => _events.Add(@event);

        public void Update(Event @event)
        {
        }

        public void Delete(Event @event)
            => _events.Remove(@event);
    }
}
