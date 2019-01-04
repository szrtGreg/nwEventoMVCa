using System;
using System.Collections.Generic;
using System.Text;

namespace nwEventoMVCa.Core.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IEventService _eventService;

        public DataInitializer(IEventService eventService)
        {
            _eventService = eventService;
        }

        public void Seed()
        {
            for (var i = 1; i < 5; i++)
            {
                var eventId = Guid.NewGuid();
                _eventService.Add(eventId, $"Event{i}", "darmowe", i*12);
                _eventService.AddTickets(eventId, 5);

            }
        }
    }
}
