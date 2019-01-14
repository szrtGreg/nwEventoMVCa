using nwEventoMVCa.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace nwEventoMVCa.Core.Services
{
    public interface IUserService
    {
        UserDto Get(Guid userId);
        UserDto Get(string email);
        void Login(string email, string password);
        void Register(string email, string password, RoleDto role);
    }
}
