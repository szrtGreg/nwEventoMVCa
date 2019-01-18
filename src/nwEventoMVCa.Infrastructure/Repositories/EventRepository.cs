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
        private static readonly ISet<Event> _events = new HashSet<Event>();
        //{
        //    new Event(Guid.NewGuid(), "Laptop", "Platne", 3000),
        //    new Event(Guid.NewGuid(), "Jeans", "darmowe", 150),
        //    new Event(Guid.NewGuid(), "Hammer", "Platne", 47)
        //};


    public Event Get(Guid id)
            => _events.SingleOrDefault(x => x.Id == id);

        public IEnumerable<Event> GetAll()
            => _events;

        public IEnumerable<Event> GetEventPage(int eventPage)
            => _events.Skip((eventPage-1) * 4).Take(4);

        public void Add(Event @event)
            => _events.Add(@event);

        public void Update(Event @event)
        {
        }

        public void Delete(Event @event)
            => _events.Remove(@event);
    }
}
