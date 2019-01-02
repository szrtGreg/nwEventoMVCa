using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using nwEventoMVCa.Core.Domain;
using nwEventoMVCa.Core.DTO;
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
        private readonly IMapper _mapper;

        public EventsController(IEventService eventService, IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var events = _eventService.GetAll().Select(e => new EventViewModel(e));

            return View(events);
        }

        [HttpGet("{id}")]
        public IActionResult Details(Guid id)
        {
            var eventDetails = _eventService.Get(id);
            if (eventDetails == null)
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<EventDetailsViewModel>(eventDetails);

            return View(viewModel);
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
            var eventId = Guid.NewGuid();
            _eventService.Add(eventId, viewModel.Name, viewModel.Category, viewModel.Price);
            _eventService.AddTickets(eventId, viewModel.TicketsCount);

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
        public IActionResult Update(AddOrUpdateEventViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            _eventService.Update(new EventDto
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Price = viewModel.Price,
                Category = viewModel.Category
            });

            return RedirectToAction(nameof(Index));
        }
    }
}
