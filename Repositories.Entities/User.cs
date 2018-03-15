using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repositories.Entities
{
    public class User : IdentityUser
    {
        [Key]
        [Column("userid")]
        public int UserID { get; set; }

        [Column("userroleid")]
        public int RoleID { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("isenabled")]
        public bool IsEnabled { get; set; }

        [Column("lockoutend")]
        public override DateTimeOffset? LockoutEnd { get; set; }

        [Column("twofactorenabled")]
        public override bool TwoFactorEnabled { get; set; }

        [Column("phonenumberconfirmed")]
        public override bool PhoneNumberConfirmed { get; set; }

        [Column("phonenumber")]
        public override string PhoneNumber { get; set; }

        [Column("concurrencystamp")]
        public override string ConcurrencyStamp { get; set; }
        [Column("securitystamp")]
        public override string SecurityStamp { get; set; }
        [Column("passwordhash")]
        public override string PasswordHash { get; set; }
        [Column("emailconfirmed")]
        public override bool EmailConfirmed { get; set; }
        [Column("normalizedemail")]
        public override string NormalizedEmail { get; set; }
        [Column("email")]
        public override string Email { get; set; }
        [Column("normalizedusername")]
        public override string NormalizedUserName { get; set; }
        [Column("username")]
        public override string UserName { get; set; }
        [Column("id")]
        public override string Id { get; set; }
        [Column("lockoutenabled")]
        public override bool LockoutEnabled { get; set; }
        [Column("accessfailedcount")]
        public override int AccessFailedCount { get; set; }

    }
}
