using Microsoft.AspNetCore.Mvc;
using nwEventoMVCa.Core.Domain;
using nwEventoMVCa.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nwEventoMVCa.Web.Controllers
{
    [Route("events")]
    public class EventsController : Controller
    {
        private static readonly List<Event> _events = new List<Event>
        {
            new Event("Laptop", "Platne", 3000),
            new Event("Jeans", "Bezplatne", 150),
            new Event("Hammer", "Platne", 47)
        };

        public IActionResult Index()
        {
            var events = _events.Select(e => new EventViewModel
            {
                Id = e.Id,
                Name = e.Name,
                Category = e.Category,
                Price = e.Price
            });

            return View(events);
        }
    }
}
