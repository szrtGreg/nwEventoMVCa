using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nwEventoMVCa.Core.Domain;
using nwEventoMVCa.Core.DTO;
using nwEventoMVCa.Core.Repositories;

namespace nwEventoMVCa.Core.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public EventDto Get(Guid id)
        {
            var @event = _eventRepository.Get(id);
            return @event == null ? null : new EventDto
            {
                Id = @event.Id,
                Name = @event.Name,
                Category = @event.Category,
                Price = @event.Price
            };
        }

        public IEnumerable<EventDto> GetAll()
        {
            var events = _eventRepository.GetAll()
                .Select(e => new EventDto
            {
                    Id = e.Id,
                    Name = e.Name,
                    Category = e.Category,
                    Price = e.Price
            });

            return events;
        }

        public void Add(string name, string category, decimal price)
        {
            var @event = new Event(name, category, price);
            _eventRepository.Add(@event);
        }

        public void Update(EventDto @eventDto)
        {
            var existingEvent = _eventRepository.Get(@eventDto.Id);
            if (existingEvent == null)
            {
                throw new Exception($"Event was not found, id: '{@eventDto.Id}'.");
            }
            existingEvent.SetName(@eventDto.Name);
            existingEvent.SetPrice(@eventDto.Price);
            existingEvent.SetCategory(@eventDto.Category);
            _eventRepository.Update(existingEvent);
        }
    }
}
