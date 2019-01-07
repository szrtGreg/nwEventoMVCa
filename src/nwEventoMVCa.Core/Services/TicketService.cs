using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using nwEventoMVCa.Core.DTO;
using nwEventoMVCa.Core.Extensions;
using nwEventoMVCa.Core.Repositories;

namespace nwEventoMVCa.Core.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public TicketService(IUserRepository userRepository, 
            IEventRepository eventRepository,
            ITicketRepository ticketRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public TicketDto GetTicket(Guid ticketId, Guid eventId, string email)
        {
            var user = _userRepository.GetOrFailUser(email);
            var ticket = _eventRepository.GetOrFailTicket(eventId, ticketId);

            return _mapper.Map<TicketDto>(ticket);
        }

        public IEnumerable<TicketDto> GetTicketsForUser(string email)
        {
            var user = _userRepository.GetOrFailUser(email);
            var tickets = _eventRepository.GetAll().SelectMany(e => e.Tickets);
            var userTickets = tickets.Where(t => t.UserId == user.Id);


            return _mapper.Map<IEnumerable<TicketDto>>(userTickets);
        }

        public void Purchase(string email, Guid eventId, int amount)
        {
            var user = _userRepository.GetOrFailUser(email);
            var @event = _eventRepository.GetOrFailEvent(eventId);
            @event.PurchaseTickets(user, 1);
            _eventRepository.Update(@event);
        }

        public void Cancel(string email, Guid eventId, int amount)
        {
            var user = _userRepository.GetOrFailUser(email);
            var @event = _eventRepository.GetOrFailEvent(eventId);
            @event.CancelPurchasedTickets(user, amount);
            _eventRepository.Update(@event);
        }
    }
}
