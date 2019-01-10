using nwEventoMVCa.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace nwEventoMVCa.Core.Repositories
{
    public interface IOrderRepository
    {
        void Add(Order order);
        Order Get(Guid id);
        IEnumerable<Order> Browse(Guid userId);
    }
}
