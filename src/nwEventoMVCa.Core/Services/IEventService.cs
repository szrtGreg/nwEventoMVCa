using nwEventoMVCa.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace nwEventoMVCa.Core.Services
{
    public interface IEventService
    {
        EventDetailsDto Get(Guid id);
        IEnumerable<EventDto> GetAll();
        void Add(Guid id, string name, string category, decimal price);
        void AddTickets(Guid eventId, int amount);
        void Update(EventDto @eventDto);
    }
}
