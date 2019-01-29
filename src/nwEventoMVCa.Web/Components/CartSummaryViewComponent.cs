using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using nwEventoMVCa.Core.Services;
using nwEventoMVCa.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nwEventoMVCa.Web.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public CartSummaryViewComponent(ICartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            var currentUserId = Guid.Parse(User.Identity.Name);
            var cart = _cartService.Get(currentUserId);
            if (cart == null)
            {
                _cartService.Create(currentUserId);
                cart = _cartService.Get(currentUserId);
            }
            var viewModel = _mapper.Map<CartViewModel>(cart);
            viewModel.ReturnUrl = null;

            return View(viewModel);
        }
    }
}
