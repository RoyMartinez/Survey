using Domain.Dto;
using Domain.Models;
using Infrastructure.Interface;
using Microsoft.Extensions.Configuration;
using Service.Helper;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Service
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _UserRepository;

        public UserService
        (
            IConfiguration configuration,
            IUserRepository UserRepository
        )
        {
            _configuration = configuration;
            _UserRepository = UserRepository;
        }

        public string Login(LoginRequestDto Request)
        {
            try
            {
                var encriptPassword = CriptografyHelper.Encrypt(Request.Password);

                var User = _UserRepository.Find(user => user.UserName == Request.UserName && user.Password == encriptPassword).FirstOrDefault();

                if (User == null) return null;

                return JwtHelper.JwtToken(Request.UserName, _configuration);
            }
            catch (Exception ex)
            {
                return $"Error {ex.Message}";
            }
        }

        public string Register(RegisterRequestDto Request)
        {
            try
            {
                var encriptPassword = CriptografyHelper.Encrypt(Request.Password);
                var newUser = new User()
                {
                    Email = Request.Email,
                    UserName = Request.UserName,
                    Password = encriptPassword,
                    IsActive = true
                };
                _UserRepository.Add(newUser);

                return JwtHelper.JwtToken(newUser.UserName, _configuration);
            }
            catch (Exception ex)
            {
                return $"Error {ex.Message}";
            }
        }
    }
}
