using nwEventoMVCa.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace nwEventoMVCa.Core.Services
{
    public interface IProductService
    {
        ProductDto Get(Guid id);
        IEnumerable<ProductDto> GetAll();
        void Add(string name, decimal price);
        void Update(ProductDto product);
    }
}
