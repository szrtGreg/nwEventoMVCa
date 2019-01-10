using nwEventoMVCa.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nwEventoMVCa.Web.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ProductViewModel(ProductDto dto)
        {
            Id = dto.Id;
            Name = dto.Name;
        }
    }
}
