using System;
using System.Collections.Generic;
using System.Text;

namespace nwEventoMVCa.Core.DTO
{
    public class EventDetailsDto : EventDto
    {
        public IEnumerable<TicketDto> Tickets { get; set; }
    }
}
