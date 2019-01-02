using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nwEventoMVCa.Web.Models
{
    public class TicketViewModel
    {
        public Guid Id { get; set; }

        public Guid EventId { get; set; }

        public int Seating { get; set; }
    }
}
