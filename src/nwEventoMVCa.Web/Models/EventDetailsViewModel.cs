using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nwEventoMVCa.Web.Models
{
    public class EventDetailsViewModel :EventViewModel
    {
        public IEnumerable<TicketViewModel> Tickets { get; set; }
    }
}
