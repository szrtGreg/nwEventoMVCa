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
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ITicketService _ticketService;
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, 
            ITicketService ticketService,
            ICartService cartService, 
            IMapper mapper)
        {
            _userService = userService;
            _ticketService = ticketService;
            _cartService = cartService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var userDto = _userService.Get(CurrentUserId);
            var viewModel = _mapper.Map<UserViewModel>(userDto);
            return View(viewModel);
         }

        [HttpGet("events")]
        public IActionResult PurchasedEvents()
        {
            var userDto = _userService.Get(CurrentUserId);
            var ticketsDto = _ticketService.GetTicketsForUser(userDto.Email);
            var viewModel = _mapper.Map<IEnumerable<TicketViewModel>>(ticketsDto);
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
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            _cartService.Create(user.Id);
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
            try
            {
                _cartService.Delete(CurrentUserId);
            }
            catch
            {
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("register")]
        [AllowAnonymous]
        public IActionResult Register()
           => View(new RegisterViewModel());

        [HttpPost("register")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            try
            {
                _userService.Register(viewModel.Email, viewModel.Password, viewModel.Role);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(viewModel);
            }
            return RedirectToAction(nameof(Login));
        }
    }
}
