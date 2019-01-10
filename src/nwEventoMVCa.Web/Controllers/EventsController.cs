using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;
        private readonly ITicketService _ticketService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public EventsController(IEventService eventService, 
            ITicketService ticketService,
            IMapper mapper, IMemoryCache cache)
        {
            _eventService = eventService;
            _ticketService = ticketService;
            _mapper = mapper;
            _cache = cache;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var events = _cache.Get<IEnumerable<EventViewModel>>("events");
            if (events == null)
            {
                Console.WriteLine("Fetching from service");
                events = _eventService.GetAll().Select(e => new EventViewModel(e));
                _cache.Set("events", events, TimeSpan.FromSeconds(20));
            }
            else
            {
                Console.WriteLine("Fetching from cache");
            }
            //var events = _eventService.GetAll().Select(e => new EventViewModel(e));

            return View(events);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "require-admin")]
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
        [Authorize(Policy = "require-admin")]
        public IActionResult Add()
        {
            var viewModel = new AddOrUpdateEventViewModel();

            return View(viewModel);
        }

        [HttpPost("add")]
        [Authorize(Policy = "require-admin")]
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
        [Authorize(Policy = "require-admin")]
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
        [Authorize(Policy = "require-admin")]
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

        [HttpGet("{id}/delete")]
        [Authorize(Policy = "require-admin")]
        public IActionResult Delete(Guid id)
        {
            _eventService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/purchase")]
        public IActionResult Purchase(Guid id)
        {
            try
            {
                var email = @User.Identity.Name;
                _ticketService.Purchase(email, id, 1);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}/cancel")]
        public IActionResult Cancel(Guid id)
        {
            try
            {
                var email = @User.Identity.Name;
                _ticketService.Cancel(email, id, 1);

                return RedirectToAction(nameof(Index), controllerName: "Account");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
