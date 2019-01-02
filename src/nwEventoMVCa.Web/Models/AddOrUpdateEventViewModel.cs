using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nwEventoMVCa.Web.Models
{
    public class AddOrUpdateEventViewModel : EventViewModel
    {
        public List<SelectListItem> Categories { get; } = new List<SelectListItem>
        {
            new SelectListItem { Text = "Platne", Value = "Platne"},
            new SelectListItem { Text = "Darmowe", Value = "Darmowe"},
        };
    }
}
