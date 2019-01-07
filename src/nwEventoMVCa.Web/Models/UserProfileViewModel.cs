using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nwEventoMVCa.Web.Models
{
    public class UserProfileViewModel
    {
        public UserViewModel UserViewModel { get; set; }
        public IEnumerable<TicketViewModel> TicketsViewModel { get; set; }
    }
}
