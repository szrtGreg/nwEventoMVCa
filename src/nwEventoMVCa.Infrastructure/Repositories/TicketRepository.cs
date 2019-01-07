using nwEventoMVCa.Core.Domain;
using nwEventoMVCa.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace nwEventoMVCa.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private static readonly ISet<Ticket> _tickets = new HashSet<Ticket>();

        public IEnumerable<Ticket> GetAll()
            => _tickets;
    }
}
