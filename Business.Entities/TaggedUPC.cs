namespace Business.Entities
{
    public class TaggedUPC
    {
        public int DescriptionID { get; set; }
        public string Description { get; set; }

        public string UPCCode { get; set; }

        public int ProductTypeID { get; set; }

        public int ProductCategoryID { get; set; }

        public int ProductSubcategoryID { get; set; }

        public string ProductSizing { get; set; }

        public bool IsMigrated { get; set; }


    }
}
