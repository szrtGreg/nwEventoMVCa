using System;
using System.Collections.Generic;
using System.Text;

namespace nwEventoMVCa.Core.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IEventService _eventService;
        private readonly IProductService _productService;


        public DataInitializer(IEventService eventService, IProductService productService)
        {
            _eventService = eventService;
            _productService = productService;
        }

        public void Seed()
        {
            for (var i = 1; i < 11; i++)
            {
                var eventId = Guid.NewGuid();
                _eventService.Add(eventId, $"Event{i}", "darmowe", i*12);
                _eventService.AddTickets(eventId, 5);

            }

            //for (var i = 1; i < 5; i++)
            //{
            //    _productService.Add($"Product{i}");
            //}
        }
    }
}
