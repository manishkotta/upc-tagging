using System;
using System.Collections.Generic;
using System.Text;

namespace Common.CommonEntities
{
    public class UPCSearchFilter
    {
        public string UPCCode { get; set; }

        public string Description { get; set; }

        public List<int> Type { get; set; }

        public List<int> ProductCategory { get; set; }

        public List<int> ProductSubcategory { get; set; }

        public string ProductSizing { get; set; }

        public List<int> Status { get; set; }

        public int SortOrder { get; set; }

        public string SortField { get; set; }

        public int Rows { get; set; }

        public int First { get; set; }

        public int UserID { get; set; }

        public int RoleID { get; set; }
    }
}
