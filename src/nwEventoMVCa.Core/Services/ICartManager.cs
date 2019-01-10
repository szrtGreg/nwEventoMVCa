using nwEventoMVCa.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace nwEventoMVCa.Core.Services
{
    public interface ICartManager
    {
        Cart Get(Guid userId);
        void Set(Guid userId, Cart cart);
        void Delete(Guid userId);
    }
}
