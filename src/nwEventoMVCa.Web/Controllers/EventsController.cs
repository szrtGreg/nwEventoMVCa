using Microsoft.AspNetCore.Mvc;
using nwEventoMVCa.Core.Domain;
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
            return View(_events);
        }
    }
}
