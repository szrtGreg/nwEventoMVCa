using nwEventoMVCa.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace nwEventoMVCa.Core.Repositories
{
    public interface IUserRepository
    {
        User Get(Guid userId);
        User Get(string email);
        void Add(User user);
    }
}
