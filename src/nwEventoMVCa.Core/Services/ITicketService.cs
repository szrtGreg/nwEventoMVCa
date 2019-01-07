using nwEventoMVCa.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace nwEventoMVCa.Core.Services
{
    public interface ITicketService
    {
        TicketDto GetTicket(Guid ticketId, Guid eventId, string email);
        IEnumerable<TicketDto> GetTicketsForUser(string email);
        void Purchase(string email, Guid eventId, int amount);
        void Cancel(string email, Guid eventId, int amount);
    }
}
