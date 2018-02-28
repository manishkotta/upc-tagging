using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CommonEntities
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty(this DataTable dt)
        {
            return dt == null ? true : dt.Rows.Count <= 0 ? true : false;
        }
    }
}
