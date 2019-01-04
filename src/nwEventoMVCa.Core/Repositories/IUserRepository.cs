using nwEventoMVCa.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace nwEventoMVCa.Core.Repositories
{
    public interface IUserRepository
    {
        User Get(string email);
        void Add(User user);
    }
}
