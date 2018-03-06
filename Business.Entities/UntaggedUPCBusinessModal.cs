using System;

namespace Business.Entities
{
    public class UntaggedUPCBusinessModal
    {
       
        public int UntaggedUPCID { get; set; }
  
        public int DescriptionID { get; set; }
        public string Description { get; set; }

        public string UPCCode { get; set; }

        public string ProductSizing { get; set; }
 
        public int? ItemAssignedTo { get; set; }

        public int? ItemAssignedBy { get; set; }

        public int? StatusID { get; set; }

        public ProductType ProductType { get; set; }

        public ProductCategory ProductCategory { get; set; }

        public ProductSubCategory ProductSubCategory { get; set; }

    }
}
