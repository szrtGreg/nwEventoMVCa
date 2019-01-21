using System;
using System.Collections.Generic;
using System.Text;

namespace nwEventoMVCa.Core.DTO
{
    public class TicketDto
    {
        public Guid Id { get; set; }

        public string EventName { get; set; }

        public Guid EventId { get; set; }

        public int Seating { get; set; }

        public Guid UserId { get; set; }

        public string Username { get; set; }
    }
}
