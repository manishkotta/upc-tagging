using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entities
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public int RoleID { get; set; }

        public string Name { get; set; }
    }
}
