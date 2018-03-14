
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Entities
{
    public class UntaggedUPC
    {
        [Column("untaggedupcid")]
        public int UntaggedUPCID { get; set; }

        [Column("descriptionid")]
        public int DescriptionID { get; set; }
        [Column("description")]
        public string Description { get; set; }

        [Column("upccode")]
        public string UPCCode { get; set; }
        [Column("producttypeid")]
        public int? ProductTypeID { get; set; }
        [Column("productcategoryid")]
        public int? ProductCategoryID { get; set; }
        [Column("productsubcategoryid")]
        public int? ProductSubcategoryID { get; set; }

        [Column("productsizing")]
        public string ProductSizing { get; set; }

        [Column("itemassingedto")]
        public int? ItemAssignedToFk { get; set; }

        [ForeignKey("itemassingedto")]
        public User ItemAssignedTo { get; set; } 

        [Column("itemassignedby")]
        public int? ItemAssignedBy { get; set; } 

        [Column("iteminsertedat",TypeName = "timestamp")]
        public DateTime? ItemInsertedAt { get; set; } = null;
        [Column("iteminsertedby")]
        public int? ItemInsertedBy { get; set; } 

        [Column("itemmodifiedat",TypeName = "timestamp")]
        public DateTime? ItemModifiedAt { get; set; } = null;
        [Column("itemmodifiedby")]
        public int? ItemModifiedBy { get; set; } 
        [Column("statusid")]
        public int? StatusID { get; set; }

        [ForeignKey("producttypeid")]
        public virtual ProductType ProductType { get; set; }

        [ForeignKey("productcategoryid")]
        public ProductCategory ProductCategory { get; set; }

        [ForeignKey("productsubcategoryid")]
        public ProductSubCategory ProductSubCategory { get; set; }

    }
}
