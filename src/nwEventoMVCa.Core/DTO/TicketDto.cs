using System;
using System.Collections.Generic;
using System.Text;

namespace nwEventoMVCa.Core.DTO
{
    public class TicketDto
    {
        public Guid Id { get; set; }

        public Guid EvnetId { get; set; }

        public int Seating { get; set; }
    }
}
