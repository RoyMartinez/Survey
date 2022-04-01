using Domain.Models;
using Infrastructure.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repository
{
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        private readonly IConfiguration _configuration;
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }
    }
}
