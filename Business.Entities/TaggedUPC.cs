namespace Business.Entities
{
    public class TaggedUPC
    {
        public long DescriptionID { get; set; }
        public string Description { get; set; }

        public string UPCCode { get; set; }

        public ProductType ProductType { get; set; }

        public ProductCategory ProductCategory { get; set; }

        public ProductSubCategory ProductSubCategory { get; set; }

        public string ProductSizing { get; set; }

        public bool IsMigrated { get; set; }


    }
}
