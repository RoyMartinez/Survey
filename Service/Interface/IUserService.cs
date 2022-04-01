using Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface IUserService
    {
        public string Login(LoginRequestDto Request);
        public string Register(RegisterRequestDto Request);
    }
}
