using nwEventoMVCa.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace nwEventoMVCa.Core.Services
{
    public interface IEventService
    {
        IEnumerable<EventDto> GetAll();
        void Add(string name, string category, decimal price);
    }
}
