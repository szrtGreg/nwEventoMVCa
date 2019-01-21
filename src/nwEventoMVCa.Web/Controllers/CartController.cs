using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using nwEventoMVCa.Core.Services;
using nwEventoMVCa.Web.Framework;
using nwEventoMVCa.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace nwEventoMVCa.Web.Controllers
{
    [Route("cart")]
    [CookieAuth]
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public CartController(ICartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        public IActionResult Index(string returnUrl)
        {
            var cart = _cartService.Get(CurrentUserId);
            if (cart == null)
            {
                _cartService.Create(CurrentUserId);
                cart = _cartService.Get(CurrentUserId);
            }
            var viewModel = _mapper.Map<CartViewModel>(cart);
            viewModel.ReturnUrl = returnUrl;

            return View(viewModel);
        }

        [HttpPost("{productId}/add")]
        public IActionResult Add(Guid productId, string returnUrl)
        {
            _cartService.AddProduct(CurrentUserId, productId);

            return RedirectToAction(nameof(Index), new { returnUrl });
        }


        [HttpGet("{productId}/delete")]
        public IActionResult Delete(Guid productId)
        {
            _cartService.DeleteProduct(CurrentUserId, productId);

            return RedirectToAction(nameof(Index));
        }
    }
}
