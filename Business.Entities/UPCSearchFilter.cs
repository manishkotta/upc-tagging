using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entities
{
    public class UPCSearchFilter
    {
        public List<string> UPCCode { get; set; }

        public List<int> Type { get; set; }

        public List<int> ProductCategory { get; set; }

        public List<int> ProductSubcategory { get; set; }

        public string ProductSizing { get; set; }

        public int UserID { get; set; }
    }
}
