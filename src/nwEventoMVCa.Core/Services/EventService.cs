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

        public EventDto Get(Guid id)
        {
            var @event = _eventRepository.Get(id);
            return @event == null ? null : _mapper.Map<EventDto>(@event);
        }

        public IEnumerable<EventDto> GetAll()
        {
            var events = _eventRepository.GetAll()
                .Select(e => _mapper.Map<EventDto>(e));

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
