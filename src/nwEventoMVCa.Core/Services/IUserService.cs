using System;
using System.Collections.Generic;
using System.Text;

namespace nwEventoMVCa.Core.Services
{
    public interface IUserService
    {
        void Login(string email, string password);
    }
}
