using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nwEventoMVCa.Core.Services;
using nwEventoMVCa.Web.Framework;
using nwEventoMVCa.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nwEventoMVCa.Web.Controllers
{
    [Route("products")]
    [CookieAuth("require-admin")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var products = _productService
                .GetAll()
                .Select(p => new ProductViewModel(p));

            return View(products);
        }
    }
}
