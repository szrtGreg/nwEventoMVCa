using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using nwEventoMVCa.Core.Domain;
using nwEventoMVCa.Core.DTO;
using nwEventoMVCa.Core.Services;
using nwEventoMVCa.Web.Framework;
using nwEventoMVCa.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nwEventoMVCa.Web.Controllers
{
    [Route("events")]
    [CookieAuth]
    public class EventsController : BaseController
    {
        private readonly IEventService _eventService;
        private readonly ITicketService _ticketService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public EventsController(IEventService eventService,
           ITicketService ticketService,
           IMapper mapper, IUserService userService)
        {
            _eventService = eventService;
            _ticketService = ticketService;
            _mapper = mapper;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpGet("{eventPage}")]
        public IActionResult Index(int eventPage)
        {
            var viewModel = new EventListWithPagination()
            {
                EventViewModels = _eventService.GetEventPage(eventPage).Select(e => new EventViewModel(e)),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = eventPage,
                    ItemsPerPage = 4,
                    TotalItems = 10
                },
                CurrentCategory = null
            };
            return View(viewModel);
        }

        [HttpGet("{eventPage}/{id}")]
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

        [HttpGet("{id}/update/{eventPage}")]
        [Authorize(Policy = "require-admin")]
        public IActionResult Update(Guid id, int  eventPage)
        {
            var @event = _eventService.Get(id);
            if (@event == null)
            {
                return NotFound();
            }
            var viewModel = new AddOrUpdateEventViewModel(@event);

            return View(viewModel);
        }

        [HttpPost("{id}/update/{eventPage}")]
        [Authorize(Policy = "require-admin")]
        public IActionResult Update(AddOrUpdateEventViewModel viewModel, int eventPage)
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

            return RedirectToAction(nameof(Index), new { eventPage });
        }

        [HttpGet("{id}/delete/{eventPage}")]
        [Authorize(Policy = "require-admin")]
        public IActionResult Delete(Guid id, int eventPage)
        {
            _eventService.Delete(id);

            return RedirectToAction(nameof(Index), new { eventPage });
        }

        [HttpPost("{id}/purchase")]
        public IActionResult Purchase(Guid id, int currentEventId)
        {
            try
            {
                var userDto = _userService.Get(CurrentUserId);
                var email = userDto.Email;
                _ticketService.Purchase(email, id, 1);

                return RedirectToAction(nameof(Index), new { eventPage = currentEventId });
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
                var userDto = _userService.Get(CurrentUserId);
                var email = userDto.Email;
                _ticketService.Cancel(email, id, 1);

                return RedirectToAction("PurchasedEvents", controllerName: "Account");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
