﻿using System;
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
    }
}