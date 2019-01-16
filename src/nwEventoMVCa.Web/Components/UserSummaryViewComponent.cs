using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using nwEventoMVCa.Core.Services;
using nwEventoMVCa.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nwEventoMVCa.Web.Components
{
    public class UserSummaryViewComponent : ViewComponent
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserSummaryViewComponent(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            var currentUserId = Guid.Parse(User.Identity.Name);
            var userDto = _userService.Get(currentUserId);
            var viewModel = _mapper.Map<UserViewModel>(userDto);

            return View(viewModel);
        }
    }
}
