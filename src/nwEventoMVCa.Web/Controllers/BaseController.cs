using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nwEventoMVCa.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected Guid CurrentUserId => User.Identity.IsAuthenticated ?
                Guid.Parse(User.Identity.Name)  : Guid.Empty;
    }
}
