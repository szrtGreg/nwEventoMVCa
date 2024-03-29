﻿using AutoMapper;
using nwEventoMVCa.Core.Domain;
using nwEventoMVCa.Core.DTO;
using nwEventoMVCa.Core.Extensions;
using nwEventoMVCa.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace nwEventoMVCa.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public UserDto Get(Guid userId)
        {
            var user = _userRepository.GetOrFailUser(userId);

            return _mapper.Map<UserDto>(user);
        }

        public UserDto Get(string email)
        {
            var user = _userRepository.Get(email);

            return user == null ? null : _mapper.Map<UserDto>(user);
        }

        public void Login(string email, string password)
        {
            var user = _userRepository.Get(email);
            if (user == null)
            {
                throw new Exception($"User '{email}' was not found.");
            }
            if (user.Password != password)
            {
                throw new Exception("Invalid password.");
            }
        }

        public void Register(string email, string password, RoleDto role)
        {
            var user = _userRepository.Get(email);
            if (user != null)
            {
                throw new Exception($"User '{email}' already exists.");
            }
            var userRole = (Role)Enum.Parse(typeof(Role), role.ToString(), true);
            user = new User(email, password, userRole);

            _userRepository.Add(user);
        }
    }
}
