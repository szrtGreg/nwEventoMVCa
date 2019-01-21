using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nwEventoMVCa.Web.Models
{
    public class CartViewModel
    {
        public IList<CartItemViewModel> Items { get; set; }
        public decimal TotalPrice { get; set; }
        public string ReturnUrl { get; set; }
    }
}
