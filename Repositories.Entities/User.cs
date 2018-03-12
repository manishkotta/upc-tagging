using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repositories.Entities
{
    public class User 
    {
        [Key]
        [Column("userid")]
        public int UserID { get; set; }
        [Column("username")]
        public string UserName { get; set; }
        [Column("email")]
        public string Email { get; set; }

        [Column("userpassword")]
        public string Password { get; set; }

        [Column("userrole")]
        public int RoleID { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
}
