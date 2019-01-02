using Microsoft.AspNetCore.Mvc.Rendering;
using nwEventoMVCa.Core.DTO;
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


        public EventViewModel()
        {

        }

        public EventViewModel(EventDto dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            Category = dto.Category;
            Price = dto.Price;
        }
    }
}
