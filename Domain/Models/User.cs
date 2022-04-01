using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public ICollection<Survey> surveys { get; set; }
    }
}
