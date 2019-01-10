using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nwEventoMVCa.Web.Models
{
    public class CartViewModel
    {
        public IList<CartItemViewModel> Items { get; } = new List<CartItemViewModel>();
        public decimal TotalPrice => Items.Sum(i => i.TotalPrice);
    }
}
