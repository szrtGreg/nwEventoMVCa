﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nwEventoMVCa.Web.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
    }
}
