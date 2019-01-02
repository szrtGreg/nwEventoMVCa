using Microsoft.AspNetCore.Mvc;
using nwEventoMVCa.Core.Domain;
using nwEventoMVCa.Core.Services;
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
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        public IActionResult Index()
        {
            var events = _eventService.GetAll().Select(e => new EventViewModel(e));

            return View(events);
        }

        [HttpGet("add")]
        public IActionResult Add()
        {
            var viewModel = new AddOrUpdateEventViewModel();

            return View(viewModel);
        }

        [HttpPost("add")]
        public IActionResult Add(AddOrUpdateEventViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            _eventService.Add(viewModel.Name, viewModel.Category, viewModel.Price);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/update")]
        public IActionResult Update(Guid id)
        {
            var @event = _eventService.Get(id);
            if (@event == null)
            {
                return NotFound();
            }
            var viewModel = new AddOrUpdateEventViewModel(@event);

            return View(viewModel);
        }

        [HttpPost("{id}/update")]
        public IActionResult Update(Guid id, AddOrUpdateEventViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
          
            return RedirectToAction(nameof(Index));
        }
    }
}
