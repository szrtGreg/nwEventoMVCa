using nwEventoMVCa.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace nwEventoMVCa.Core.Repositories
{
    public interface IEventRepository
    {
        Event Get(Guid id);
        IEnumerable<Event> GetAll(string name);
        IEnumerable<Event> GetEventPage(int eventPage);
        void Add(Event @event);
        void Update(Event @event);
        void Delete(Event @event);
    }
}
