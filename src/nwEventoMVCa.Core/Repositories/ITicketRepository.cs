using nwEventoMVCa.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace nwEventoMVCa.Core.Repositories
{
    public interface ITicketRepository
    {
        IEnumerable<Ticket> GetAll();
    }
}
