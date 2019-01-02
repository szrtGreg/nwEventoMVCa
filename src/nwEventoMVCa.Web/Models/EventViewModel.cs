using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nwEventoMVCa.Web.Models
{
    public class EventViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        public List<SelectListItem> Categories { get; } = new List<SelectListItem>
        {
            new SelectListItem { Text = "Platne", Value = "Platne"},
            new SelectListItem { Text = "Darmowe", Value = "Darmowe"},
        };
    }
}
