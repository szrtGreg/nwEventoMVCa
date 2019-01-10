using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using nwEventoMVCa.Core.Services;
using nwEventoMVCa.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace nwEventoMVCa.Web.Controllers
{
    [Route("account")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITicketService _ticketService;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, 
            ITicketService ticketService,
            IMemoryCache cache,
            IMapper mapper)
        {
            _userService = userService;
            _ticketService = ticketService;
            _cache = cache;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var userDto = _userService.Get(@User.Identity.Name);
            var ticketsDto = _ticketService.GetTicketsForUser(userDto.Email);

            var viewModel = new UserProfileViewModel()
            {
                UserViewModel = _mapper.Map<UserViewModel>(userDto),
                TicketsViewModel = _mapper.Map<IEnumerable<TicketViewModel>>(ticketsDto)
            };

            return View(viewModel);
         }

        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login()
           => View();

        [AllowAnonymous]
        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            try
            {
                _userService.Login(viewModel.Email, viewModel.Password);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View(viewModel);
            }

            var user = _userService.Get(viewModel.Email);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, viewModel.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            _cache.Set($"{viewModel.Email}:cart", new CartViewModel(), DateTime.UtcNow.AddDays(7));
            return RedirectToAction("Index", "Account");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            await HttpContext.SignOutAsync();
            _cache.Remove($"{User.Identity.Name}:cart");
            return RedirectToAction("Index", "Home");
        }
    }
}
