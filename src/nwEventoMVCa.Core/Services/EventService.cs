using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using nwEventoMVCa.Core.Domain;
using nwEventoMVCa.Core.DTO;
using nwEventoMVCa.Core.Extensions;
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

        public IEnumerable<EventDto> GetAll(string name)
        {
            var events = _eventRepository.GetAll(name)
                .Select(e => _mapper.Map<EventDto>(e));

            return events;
        }

        public IEnumerable<EventDto> GetEventPage(int eventPage)
        {
            var events = _eventRepository.GetEventPage(eventPage)
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
            var @event = _eventRepository.GetOrFailEvent(eventId);
            @event.AddTickets(amount);
            _eventRepository.Update(@event);
        }

        public void Update(EventDto @eventDto)
        {
            var existingEvent = _eventRepository.GetOrFailEvent(@eventDto.Id);
            existingEvent.SetName(@eventDto.Name);
            existingEvent.SetPrice(@eventDto.Price);
            existingEvent.SetCategory(@eventDto.Category);
            _eventRepository.Update(existingEvent);
        }

        public void Delete(Guid id)
        {
            var @event = _eventRepository.GetOrFailEvent(id);
            _eventRepository.Delete(@event);
        }
    }
}
