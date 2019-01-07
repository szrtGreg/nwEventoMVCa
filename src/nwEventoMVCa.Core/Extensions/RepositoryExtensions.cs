using nwEventoMVCa.Core.Domain;
using nwEventoMVCa.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nwEventoMVCa.Core.Extensions
{
    public static class RepositoryExtensions
    {
        public static Event GetOrFailEvent(this IEventRepository repo, Guid id)
        {
            var @event = repo.Get(id);
            if (@event == null)
            {
                throw new Exception($"Event was not found, id: '{id}'.");
            }

            return @event;
        }

        public static User GetOrFailUser(this IUserRepository repo, string email)
        {
            var user = repo.Get(email);
            if (user == null)
            {
                throw new Exception($"Event was not found, email: '{email}'.");
            }

            return user;
        }

        public static Ticket GetOrFailTicket(this IEventRepository repo, Guid eventId, Guid ticketId)
        {
            var @event = repo.GetOrFailEvent(eventId);
            var ticket = @event.Tickets.SingleOrDefault(x => x.Id == ticketId);
            if (ticket == null)
            {
                throw new Exception($"Ticket was not found");
            }

            return ticket;
        }
    }
}
