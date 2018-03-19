using System;
using System.Collections.Generic;
using System.Text;

namespace Common.CommonEntities
{
    public class UserToken
    {
        public string FullName { get; set; } = string.Empty;

        public int RoleID { get; set; }

        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            return this.Id == ((UserToken)obj).Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
