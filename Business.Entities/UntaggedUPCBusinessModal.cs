using System;

namespace Business.Entities
{
    public class UntaggedUPCBusinessModal
    {
       
        public int UntaggedUPCID { get; set; }
  
        public int DescriptionID { get; set; }
        public string Description { get; set; }

        public string UPCCode { get; set; }

        public int? ProductTypeID { get; set; }

        public int? ProductCategoryID { get; set; }

        public int? ProductSubcategoryID { get; set; }

        public string ProductSizing { get; set; }
 
        public int? ItemAssignedTo { get; set; }

        public int? ItemAssignedBy { get; set; }

        public int? StatusID { get; set; }

        public string ProductType { get; set; }

        public string ProductCategory { get; set; }

        public string ProductSubCategory { get; set; }

    }
}
