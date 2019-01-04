using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using nwEventoMVCa.Core.Domain;
using nwEventoMVCa.Core.DTO;
using nwEventoMVCa.Core.Repositories;

namespace nwEventoMVCa.Core.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public EventDetailsDto Get(Guid id)
        {
            var @event = _eventRepository.Get(id);

            return _mapper.Map<EventDetailsDto>(@event);
        }

        public IEnumerable<EventDto> GetAll()
        {
            var events = _eventRepository.GetAll()
                .Select(e => _mapper.Map<EventDto>(e));

            return events;
        }

        public void Add(Guid id, string name, string category, decimal price)
        {
            var @event = new Event(id, name, category, price);
            _eventRepository.Add(@event);
        }

        public void AddTickets(Guid eventId, int amount)
        {
            var @event = _eventRepository.Get(eventId);
            if (@event == null)
            {
                throw new Exception($"Event was not found for id: '{@event.Id}'");
            }
            @event.AddTickets(amount);
            _eventRepository.Update(@event);
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

        public void Delete(Guid id)
        {
            var @event = _eventRepository.Get(id);
            if (@event == null)
            {
                throw new Exception($"Event was not found, id: '{id}'.");
            }
            _eventRepository.Delete(@event);
        }
    }
}
