using System;
using System.Collections.Generic;
using System.Text;
using nwEventoMVCa.Core.DTO;

namespace nwEventoMVCa.Core.Services
{
    public class OrderService : IOrderService
    {
        public IEnumerable<OrderDto> Browse(Guid userId)
        {
            throw new NotImplementedException();
        }

        public void Create(Guid userId)
        {
            throw new NotImplementedException();
        }

        public OrderDto Get(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
